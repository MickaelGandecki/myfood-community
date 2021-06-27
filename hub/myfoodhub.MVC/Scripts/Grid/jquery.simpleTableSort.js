/**
 * Easiest way to sort your tables
 *
 * For further information and documentation go to https://github.com/dan-lee/jquery-easyTableSort
 *
 * Copyright (c) 2012 Daniel Lehr <daniellehr@gmx.de>, agixo <http://agixo.de>
 * Released under the MIT license.
 * See MIT-LICENSE.txt
 */

(function($) {
  var defaults = {
    /**
     * Changes the prefix of the used css classes in the plugin.
     * These classes will be appended to the head columns to determine which state they were.
     * If the prefix 'sort' is already used in your project you can change it here
     */
    prefix: 'sort',

    /**
     * The sort order in which your table entries will be sorted first.
     * Possible entries:
     *  - asc: Ascending order (a-z, 0-9, ...)
     *  - desc: Descending order (z-a, 9-0, ...)
     */
    order: 'asc',

    /**
     * If the table is dynamic, meaning the table gets updated by AJAX or something else, this setting should be set to true.
     * Otherwise the initial table data is restored.
     *
     * This is for performance reason: If it's a static table the rows don't have to be reprocessed every sort change.
     */
    dynamic: false,

    /**
     * With this option set to 'true' the state classes of the columns (e.g. 'sort-asc' and 'sort-desc') won't be removed when a different column is sorted.
     * However, the last sort state is never 'forgotten', it will always be the opposite of the last sorted (or the default)
     */
    multiSortStates: false,

    /**
     * Adds the possibility to sort the table entries when the table is created.
     * Accepts index values of table head column (zero-based).
     */
    autoSort: null,

    /**
     * You can add your own sort methods here.
     *
     * Example: Change "dd-mm-yyy" formatted date to JavaScript compatible "mm-dd-yyyy":
     * sortMethods: {
     *   myDate: function(a, b) {
     *     a = a.split('.');
     *     a = a[1]+'.'+a[0]+'.'+a[2];
     *
     *     b = b.split('.');
     *     b = b[1]+'.'+b[0]+'.'+b[2];
     *
     *     return new Date(a) > new Date(b) ? 1 : -1;
     *   }
     * }
     */
    sortMethods: {
      numeric: function(a, b) {
        return (parseInt(a) - parseInt(b));
      },
      float: function(a, b) {
        return (parseFloat(a) - parseFloat(b));
      },
      alphabetical: function(a, b) {
        return (a.toLowerCase() > b.toLowerCase()) ? 1 : -1;
      },
		date: function (a, b) {
			console.log(a);
        a = new Date(a);
        b = new Date(b);

        if (priv.helper.isDate(a) && priv.helper.isDate(b)) {
          return (a > b) ? 1 : -1;
        }
        return 0;
      }
    },

    /**
     * If you have no control over the table structure and it lacks of a table head, then set this value to true
     * and the first row of your table will be treatened as table head column
     */
    fixTableHead: false,

    /**
     * Table head columns can be excluded from to be sorted by adding the index.
     *
     * Example which excludes the second row (zero-based):
     * excludeSortColumns: [ 1 ]
     */
    excludeSortColumns: [],

    /**
     *
     */
    onBeforeSort: function () {},

    /**
     *
     */
    onAfterSort: function () {}
  };

  /**
   * @param element
   * @param options
   * @constructor
   */
  function Plugin(element, options) {
    priv.init(element, options);
  }

  var priv = {
    /**
     * Doing initial stuff
     *
     * @param element
     * @param options
     */
    init: function(element, options) {
      this.options = $.extend(true, {}, defaults, options);

      this.table = $(element);

        // If enabled, fix the table head structure before processing the rows and columns
      if (this.options.fixTableHead === true) {
        this.fixTableHead();
      }

      this.rows = this.table.find('tbody').children();
      this.rowLength = this.rows.length;
      this.cols = this.table.find('thead').find('th');

      this.sortOrder = new Array(this.cols.length);
      this.sortModes = ['asc', 'desc'];

      var self = this;
      this.cols.on('click', function() {
        // Call this in a closure to maintain the scope of the 'sort' method
        self.sort(this);
      });

      // Append '-' to the prefix if necessary. So a class name is always a string like 'prefix-suffix'
      this.options.prefix += (this.options.prefix.slice(-1) !== '-' ? '-' : '');

      // Remove all sort-* class names from the columns which are excluded form sorting
      $.each(this.options.excludeSortColumns, function(i, val) {
        var col = self.cols.eq(val);
        var newClass = col.prop('class').replace(new RegExp('\\s?\\b'+self.options.prefix+'[^\\s]+\\s?', 'g'), '');
        col.prop('class', newClass);
      });

      // Call this after all options are processed to ensure that sorting is possible
      if (this.options.autoSort !== null) {
        this.cols.eq(parseInt(this.options.autoSort)).trigger('click');
      }
    },

    /**
     * see option 'fixTableHead'
     */
    fixTableHead: function() {
      var thead = $('<thead></thead>');
      var child = this.table.find('tr').first().remove();
      this.table.prepend(thead.append(child));
    },

    /**
     * Toggles the internal sort state of every head column, and also changes the "external" state representation.
     * This means that class names are appended/removed by the state. So it's easy to style the columns with css
     * based on their class names.
     *
     * @param element
     * @param columnIndex
     */
    toggleOrder: function(element, columnIndex) {
      var currentOrder = this.sortOrder[columnIndex];
      var newKey, oldKey;

      // Remove sort classes from all head columns if multi sort states is deactivated
      if (this.options.multiSortStates === false) {
        this.cols.removeClass(this.options.prefix+'asc').removeClass(this.options.prefix+'desc');
      }

      // This case occurs when this column wasn't sorted before
      if (typeof currentOrder === 'undefined') {
        // Find out the index of the 'toggle array' by the default sort order
        newKey = this.helper.getIndexByValue(this.sortModes, this.options.order);
        // Set the order initially, so on the next sorting process the order can be toggled
        this.sortOrder[columnIndex] = this.sortModes[newKey];
        $(element).addClass(this.options.prefix + this.sortModes[newKey]);
      } else {
        oldKey = this.helper.getIndexByValue(this.sortModes, currentOrder);
        // Little trick for toggling two values in an array (there may be only two values in this array!):
        // 1. cast to bool, 2. negate the value, 3. cast back to int
        newKey = +!oldKey;
        this.sortOrder[columnIndex] = this.sortModes[newKey];

        $(element).removeClass(this.options.prefix + this.sortModes[oldKey]).addClass(this.options.prefix + this.sortModes[newKey]);
      }
    },

    /**
     * Returns, based on the options, whether this is an excluded column or not.
     * Also allows negative values. (Means: starting from the end)
     *
     * @param current
     * @return {boolean}
     */
    isExcluded: function(current) {
      var len = this.cols.length;

      var excluded = false;
      $.each(this.options.excludeSortColumns, function(i, val) {
        val += val < 0 ? len : 0;

        if (current === val) {
          excluded = true;
        }
      });
      return excluded;
    },

    /**
     * The actual sort algorithm is executed here.
     *
     * @param method
     * @param columnIndex
     */
    sortBy: function(method, columnIndex) {
      var self = this;
      this.rows.sort(function(a, b) {
        // Take the text of the rows for the comparison
        a = $(a).find('td').eq(columnIndex).text();
        b = $(b).find('td').eq(columnIndex).text();

        // If one of value of a and b is falsey, don't bother to compare them.
        if (!a || !b) {
          return 0;
        }

        // The return value of the actual sort algorithm is multiplied by the current sort order, but inverted
        // 'asc' turns to 'desc' (1 => -1)
        return self.options.sortMethods[method](a, b) * (self.sortOrder[columnIndex] === self.sortModes[0] ? 1 : -1);
      });
    },

    /**
     * If the table is set to dynamic, then reprocess the table before each sort process
     */
    updateRows: function() {
      if (this.options.dynamic === true) {
        var newChildren = this.table.find('tbody').children(),
            newRowLength = newChildren.length;

        if (newRowLength != this.rowLength) {
          this.rows = newChildren;
          this.rowLength = newRowLength;
        }
      }
    },

    /**
     * Is called when a table head column is clicked
     *
     * @param element
     * @return {boolean}
     */
    sort: function(element) {
      var columnIndex = this.cols.index(element);

      // Check if there's a class starting with the given prefix
      if (new RegExp('\\b' + this.options.prefix).test(element.className)) {
        var method = element.className.match(new RegExp(this.options.prefix + '([^\\s]+)'))[1];

        if (this.options.sortMethods.hasOwnProperty(method)) {
          if (this.isExcluded(columnIndex)) {
            return false;
          }

          // This user defined event is called before sorting
          this.options.onBeforeSort.call($(element), columnIndex);

          this.updateRows();
          this.toggleOrder(element, columnIndex);
          this.sortBy(method, columnIndex);
          this.render();

          // This user defined event is called after sorting
          this.options.onAfterSort.call($(element), columnIndex);

          return true;
        } else console.error('No suitable sort method for '+element.className+' found.');
      }
      return false;
    },

    /**
     * Re-renders the table after it's sorted
     */
    render: function() {
      $.each(this.rows, function(i, val) {
        priv.table.append(val);
      });
    },

    helper: {
      isDate: function(d) {
        // Taken from http://stackoverflow.com/a/1353711/612202
        if (Object.prototype.toString.call(d) === '[object Date]') {
          return !isNaN(d.getTime());
        } else {
          return false;
        }
      },

      getIndexByValue: function(array, value) {
        var result = false;
        $.each(array, function(index, item) {
          if (value === item) {
            result = index;
            return false;
          }
        });
        return result;
      }
    }
  };


  $.fn.simpleTableSort = function(options) {
    return this.each(function() {
      if (!$.data(this, 'plugin_simpleTableSort')) {
        $.data(this, 'plugin_simpleTableSort', new Plugin(this, options));
      }
    });
  };
})(jQuery);
