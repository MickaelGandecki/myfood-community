using Newtonsoft.Json;
using System.Collections.Generic;

namespace myfoodapp.Hub.Business
{
    public class GrowingModel
    {
        public string modelName;
        public string type;
        public string options;
        public List<ProductionMonth> productionMonths;

        private static GrowingState seed = new GrowingState() { Id = 0, name = "[[[Seed]]]" };
        private static GrowingState grow = new GrowingState() { Id = 1, name = "[[[Grow]]]" };
        private static GrowingState harvest = new GrowingState() { Id = 2, name = "[[[Harvest]]]" };
        private static GrowingState paused = new GrowingState() { Id = 3, name = "[[[Paused]]]" };

        private static Month january = new Month() { Id = 0, name = "[[[January]]]" };
        private static Month february = new Month() { Id = 1, name = "[[[February]]]" };
        private static Month march = new Month() { Id = 2, name = "[[[March]]]" };
        private static Month april = new Month() { Id = 3, name = "[[[April]]]" };
        private static Month may = new Month() { Id = 4, name = "[[[May]]]" };
        private static Month june = new Month() { Id = 5, name = "[[[June]]]" };
        private static Month july = new Month() { Id = 6, name = "[[[July]]]" };
        private static Month august = new Month() { Id = 7, name = "[[[August]]]" };
        private static Month september = new Month() { Id = 8, name = "[[[September]]]" };
        private static Month october = new Month() { Id = 9, name = "[[[October]]]" };
        private static Month november = new Month() { Id = 10, name = "[[[November]]]" };
        private static Month december = new Month() { Id = 11, name = "[[[December]]]" };

        private static Position south = new Position() { Id = 0, name = "[[[South]]]" };
        private static Position north = new Position() { Id = 1, name = "[[[North]]]" };
        private static Position top = new Position() { Id = 2, name = "[[[Top]]]" };
        private static Position down = new Position() { Id = 3, name = "[[[Down]]]" };
        private static Position medium = new Position() { Id = 4, name = "[[[Medium]]]" };

        private static Position outerarea1 = new Position() { Id = 2, name = "[[[Outer area]]] #1" };
        private static Position centerarea = new Position() { Id = 3, name = "[[[Center area]]]" };
        private static Position outerarea2 = new Position() { Id = 4, name = "[[[Outer area]]] #2" };

        private static Vegetable tomato = new Vegetable() { name = "[[[Tomato]]]", itemPerSlot = 3, imagePath = "tomato.jpg" };
        private static Vegetable lettuce = new Vegetable() { name = "[[[Lettuce]]]", itemPerSlot = 8, imagePath = "lettuce.jpg" };
        private static Vegetable pepper = new Vegetable() { name = "[[[Pepper]]]", itemPerSlot = 4, imagePath = "pepper.jpg" };
        private static Vegetable eggplant = new Vegetable() { name = "[[[Eggplant]]]", itemPerSlot = 3, imagePath = "eggplant.jpg" };
        private static Vegetable cucumber = new Vegetable() { name = "[[[Cucumber]]]", itemPerSlot = 3, imagePath = "cucumber.jpg" };
        private static Vegetable edibleFlower = new Vegetable() { name = "[[[Edible Flower]]]", itemPerSlot = 8, imagePath = "edible-flower.jpg" };
        private static Vegetable kale = new Vegetable() { name = "[[[Kale]]]", itemPerSlot = 4, imagePath = "kale.jpg" };
        private static Vegetable mesclun = new Vegetable() { name = "[[[Mesclun]]]", itemPerSlot = 4, imagePath = "mesclun.jpg" };
        private static Vegetable strawberry = new Vegetable() { name = "[[[Strawberry]]]", itemPerSlot = 8, imagePath = "strawberry.jpg" };
        private static Vegetable purslane = new Vegetable() { name = "[[[Purslane]]]", itemPerSlot = 8, imagePath = "purslane.jpg" };
        private static Vegetable whiteOnion = new Vegetable() { name = "[[[White Onion]]]", itemPerSlot = 15, imagePath = "white-onion.jpg" };
        private static Vegetable leek = new Vegetable() { name = "[[[Leek]]]", itemPerSlot = 30, imagePath = "leek.jpg" };
        private static Vegetable chard = new Vegetable() { name = "[[[Chard]]]", itemPerSlot = 5, imagePath = "chard.jpg" };
        private static Vegetable cabbageCabu = new Vegetable() { name = "[[[Cabbage Cabu]]]", itemPerSlot = 3, imagePath = "cabbage-cabu.jpg" };
        private static Vegetable carrot = new Vegetable() { name = "[[[Carrot]]]", itemPerSlot = 100, imagePath = "carrot.jpg" };
        private static Vegetable turnip = new Vegetable() { name = "[[[Turnip]]]", itemPerSlot = 40, imagePath = "turnip.jpg" };
        private static Vegetable spinach = new Vegetable() { name = "[[[Spinach]]]", itemPerSlot = 12, imagePath = "spinach.jpg" };
        private static Vegetable argulaSalad = new Vegetable() { name = "[[[Argula Salad]]]", itemPerSlot = 4, imagePath = "argula-salad.jpg" };
        private static Vegetable celery = new Vegetable() { name = "[[[Celery]]]", itemPerSlot = 6, imagePath = "celery.jpg" };
        private static Vegetable parsnip = new Vegetable() { name = "[[[Parsnip]]]", itemPerSlot = 40, imagePath = "parsnip.jpg" };
        private static Vegetable blackRadish = new Vegetable() { name = "[[[Black Radish]]]", itemPerSlot = 40, imagePath = "black-radish.jpg" };
        private static Vegetable dwarfBean = new Vegetable() { name = "[[[Dwarf Bean]]]", itemPerSlot = 10, imagePath = "dwarf-bean.jpg" };
        private static Vegetable dwarfPeas = new Vegetable() { name = "[[[Dwarf Peas]]]", itemPerSlot = 10, imagePath = "dwarf-peas.jpg" };
        private static Vegetable beetroot = new Vegetable() { name = "[[[Beetroot]]]", itemPerSlot = 30, imagePath = "beetroot.jpg" };
        private static Vegetable cauliflower = new Vegetable() { name = "[[[Cauliflower]]]", itemPerSlot = 3, imagePath = "cauliflower.jpg" };
        private static Vegetable cabbageRave = new Vegetable() { name = "[[[Cabbage Rave]]]", itemPerSlot = 5, imagePath = "cabbage-rave.jpg" };
        private static Vegetable sweetPotatoes = new Vegetable() { name = "[[[Sweet Potatoes]]]", itemPerSlot = 4, imagePath = "sweet-potatoes.jpg" };
        private static Vegetable broadBean = new Vegetable() { name = "[[[Broad Bean]]]", itemPerSlot = 10, imagePath = "broad-bean.jpg" };
        private static Vegetable fennel = new Vegetable() { name = "[[[Fennel]]]", itemPerSlot = 4, imagePath = "fennel.jpg" };
        private static Vegetable lamblettuce = new Vegetable() { name = "[[[Lamb Lettuce]]]", itemPerSlot = 4, imagePath = "lamb-lettuce.jpg" };
        private static Vegetable zucchini = new Vegetable() { name = "[[[Zucchini]]]", itemPerSlot = 2, imagePath = "zucchini.jpg" };
        private static Vegetable cabbageBruxelles = new Vegetable() { name = "[[[Cabbage Bruxelles]]]", itemPerSlot = 2, imagePath = "cabbage-bruxelles.jpg" };
        private static Vegetable chineseCabbage = new Vegetable() { name = "[[[Chinese Cabbage]]]", itemPerSlot = 1, imagePath = "chinese-cabbage.jpg" };
        private static Vegetable melon = new Vegetable() { name = "[[[Melon]]]", itemPerSlot = 1, imagePath = "melon.jpg" };
        private static Vegetable waterMelon = new Vegetable() { name = "[[[Water Melon]]]", itemPerSlot = 1, imagePath = "water-melon.jpg" };

        private static Vegetable ramialChippedWood = new Vegetable() { name = "[[[Ramial Chipped Wood]]]", itemPerSlot = 1, imagePath = "ramial-chipped-wood.jpg" };

        public void GenerateModel()
        {
            var model = new GrowingModel();
            model.modelName = "Family22 Annual Planning v.1";
            model.type = "Family22";
            model.options = "24 x Zipgrow Towers | 5 x Perma Beds | 1 x Aerospring";

            List<ProductionMonth> productionMonths = new List<ProductionMonth>();

            var january = GetJanuary();
            var february = GetFebruary();
            var mars = GetMarch();
            var april = GetApril();
            var may = GetMay();
            var june = GetJune();
            var july = GetJuly();
            var august = GetAugust();
            var september = GetSeptember();
            var october = GetOctober();
            var november = GetNovember();
            var december = GetDecember();

            productionMonths.Add(january);
            productionMonths.Add(february);
            productionMonths.Add(mars);
            productionMonths.Add(april);
            productionMonths.Add(may);
            productionMonths.Add(june);
            productionMonths.Add(july);
            productionMonths.Add(august);
            productionMonths.Add(september);
            productionMonths.Add(october);
            productionMonths.Add(november);
            productionMonths.Add(december);

            model.productionMonths = new List<ProductionMonth>();
            model.productionMonths = productionMonths;

            var XX = JsonConvert.SerializeObject(model);
        }

        private static ProductionMonth GetJanuary()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = january;

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 180;

            kale.growingState = harvest;
            kale.productionQuantity = 180;

            mesclun.growingState = harvest;
            mesclun.productionQuantity = 180;

            purslane.growingState = harvest;
            purslane.productionQuantity = 100;

            leek.growingState = grow;
            leek.productionQuantity = 0;

            cabbageCabu.growingState = grow;
            cabbageCabu.productionQuantity = 0;

            chard.growingState = harvest;
            chard.productionQuantity = 200;

            spinach.growingState = grow;
            spinach.productionQuantity = 180;

            chard.growingState = harvest;
            chard.productionQuantity = 200;

            argulaSalad.growingState = harvest;
            argulaSalad.productionQuantity = 200;

            cauliflower.growingState = harvest;
            cauliflower.productionQuantity = 1000;

            whiteOnion.growingState = harvest;
            whiteOnion.productionQuantity = 30;

            blackRadish.growingState = harvest;
            blackRadish.productionQuantity = 200;

            fennel.growingState = harvest;
            fennel.productionQuantity = 300;

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = grow;
            chineseCabbage.productionQuantity = 0;

            ramialChippedWood.growingState = paused;
            ramialChippedWood.productionQuantity = 0;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = lettuce.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = lettuce.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = lettuce.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = kale.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = mesclun.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = purslane.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = kale.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = cabbageCabu.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = chard.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = spinach.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = argulaSalad.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = cabbageRave.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = cauliflower.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = medium;
            slot_perma_10.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = chard.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = grow;
            lettuce.productionQuantity = 0;

            purslane.growingState = grow;
            purslane.productionQuantity = 0;

            cabbageRave.growingState = grow;
            cabbageRave.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce.ShallowCopy();
            slot_aero_1.isSeed = false;
            slot_aero_1.oneCropPerSlot = true;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = cabbageRave.ShallowCopy();
            slot_aero_2.isSeed = false;
            slot_aero_2.oneCropPerSlot = true;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = purslane.ShallowCopy();
            slot_aero_3.isSeed = false;
            slot_aero_3.oneCropPerSlot = true;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = purslane.ShallowCopy();
            slot_aero_4.isSeed = false;
            slot_aero_4.oneCropPerSlot = true;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = edibleFlower.ShallowCopy();
            slot_aero_5.isSeed = false;
            slot_aero_5.oneCropPerSlot = true;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = edibleFlower.ShallowCopy();
            slot_aero_6.isSeed = false;
            slot_aero_6.oneCropPerSlot = true;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = edibleFlower.ShallowCopy();
            slot_aero_7.isSeed = false;
            slot_aero_7.oneCropPerSlot = true;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_8.isSeed = false;
            slot_aero_8.oneCropPerSlot = true;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_9.isSeed = false;
            slot_aero_9.oneCropPerSlot = true;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_10.isSeed = false;
            slot_aero_10.oneCropPerSlot = true;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = cabbageRave.ShallowCopy();
            slot_aero_11.isSeed = false;
            slot_aero_11.oneCropPerSlot = true;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = parsnip.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = fennel.ShallowCopy();
            slot_incu_2.isSeed = true;

            //var slot_incu_3 = new Slot();
            //slot_incu_3.id = "[[[Incubator]]] #3";
            //slot_incu_3.number = 8;
            //slot_incu_3.position = outerarea1;
            //slot_incu_3.vegetable = purslane.ShallowCopy();
            //slot_incu_3.oneCropPerSlot = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = cabbageRave.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = chineseCabbage.ShallowCopy();
            slot_incu_5.isSeed = true;

            //var slot_incu_6 = new Slot();
            //slot_incu_6.id = "[[[Incubator]]] #6";
            //slot_incu_6.number = 8;
            //slot_incu_6.position = centerarea;
            //slot_incu_6.vegetable = edibleFlower.ShallowCopy();
            //slot_incu_6.oneCropPerSlot = true;

            //var slot_incu_7 = new Slot();
            //slot_incu_7.id = "[[[Incubator]]] #7";
            //slot_incu_7.number = 8;
            //slot_incu_7.position = outerarea2;
            //slot_incu_7.vegetable = purslane.ShallowCopy();
            //slot_incu_7.oneCropPerSlot = true;

            //var slot_incu_8 = new Slot();
            //slot_incu_8.id = "[[[Incubator]]] #8";
            //slot_incu_8.number = 8;
            //slot_incu_8.position = outerarea2;
            //slot_incu_8.vegetable = edibleFlower.ShallowCopy();
            //slot_incu_8.oneCropPerSlot = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = cabbageCabu.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            //incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            //incubSlots.Add(slot_incu_6);
            //incubSlots.Add(slot_incu_7);
            //incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetFebruary()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = february;

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 180;

            kale.growingState = harvest;
            kale.productionQuantity = 180;

            mesclun.growingState = harvest;
            mesclun.productionQuantity = 180;

            purslane.growingState = harvest;
            purslane.productionQuantity = 100;

            leek.growingState = grow;
            leek.productionQuantity = 0;

            cabbageCabu.growingState = harvest;
            cabbageCabu.productionQuantity = 1500;

            chard.growingState = harvest;
            chard.productionQuantity = 200;

            spinach.growingState = harvest;
            spinach.productionQuantity = 180;

            chard.growingState = harvest;
            chard.productionQuantity = 200;

            argulaSalad.growingState = harvest;
            argulaSalad.productionQuantity = 200;

            cauliflower.growingState = harvest;
            cauliflower.productionQuantity = 1000;

            whiteOnion.growingState = harvest;
            whiteOnion.productionQuantity = 60;

            blackRadish.growingState = harvest;
            blackRadish.productionQuantity = 200;

            fennel.growingState = harvest;
            fennel.productionQuantity = 300;

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = grow;
            chineseCabbage.productionQuantity = 0;

            ramialChippedWood.growingState = paused;
            ramialChippedWood.productionQuantity = 0;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = lettuce.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = lettuce.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = lettuce.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = kale.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = mesclun.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = purslane.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = kale.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 300;

            cabbageBruxelles.growingState = harvest;
            cabbageBruxelles.productionQuantity = 500;

            purslane.growingState = harvest;
            purslane.productionQuantity = 200;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = cabbageCabu.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = chard.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = spinach.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = argulaSalad.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = lettuce.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = cauliflower.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = medium;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = purslane.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = seed;
            lettuce.productionQuantity = 0;

            edibleFlower.growingState = seed;
            edibleFlower.productionQuantity = 0;

            purslane.growingState = seed;
            purslane.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce.ShallowCopy();
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = cabbageRave.ShallowCopy();
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = purslane.ShallowCopy();
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = purslane.ShallowCopy();
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = edibleFlower.ShallowCopy();
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = edibleFlower.ShallowCopy();
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = edibleFlower.ShallowCopy();
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = cabbageRave.ShallowCopy();
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = parsnip.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = carrot.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = mesclun.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = blackRadish.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = tomato.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = pepper.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = whiteOnion.ShallowCopy();
            slot_incu_7.isSeed = true;

            var slot_incu_8 = new Slot();
            slot_incu_8.id = "[[[Incubator]]] #8";
            slot_incu_8.number = 8;
            slot_incu_8.position = outerarea2;
            slot_incu_8.vegetable = chard.ShallowCopy();
            slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = cabbageCabu.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetMarch()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = march;

            tomato.growingState = seed;
            tomato.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.productionQuantity = 0;

            edibleFlower.growingState = seed;
            edibleFlower.productionQuantity = 0;

            strawberry.growingState = seed;
            strawberry.productionQuantity = 0;

            leek.growingState = harvest;
            leek.productionQuantity = 100;

            blackRadish.growingState = harvest;
            blackRadish.productionQuantity = 60;

            chard.growingState = harvest;
            chard.productionQuantity = 200;

            dwarfBean.growingState = seed;
            chard.productionQuantity = 0;

            dwarfPeas.growingState = seed;
            dwarfPeas.productionQuantity = 0;

            beetroot.growingState = seed;
            beetroot.productionQuantity = 0;

            cabbageRave.growingState = harvest;
            cabbageRave.productionQuantity = 250;

            whiteOnion.growingState = harvest;
            whiteOnion.productionQuantity = 10;

            fennel.growingState = harvest;
            fennel.productionQuantity = 150;

            zucchini.growingState = seed;
            zucchini.productionQuantity = 0;

            // AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = chard.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = dwarfPeas.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = beetroot.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = cabbageRave.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = medium;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = medium;
            slot_perma_10.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 3;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = blackRadish.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = zucchini.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 250;

            purslane.growingState = harvest;
            purslane.productionQuantity = 150;

            cabbageRave.growingState = harvest;
            cabbageRave.productionQuantity = 200;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 120;

            chineseCabbage.growingState = harvest;
            chineseCabbage.productionQuantity = 250;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce.ShallowCopy();
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = cabbageRave.ShallowCopy();
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = purslane.ShallowCopy();
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = purslane.ShallowCopy();
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = edibleFlower.ShallowCopy();
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = edibleFlower.ShallowCopy();
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = edibleFlower.ShallowCopy();
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = chineseCabbage.ShallowCopy();
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = cabbageRave.ShallowCopy();
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.itemPerSlot = 1;
            cucumber.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.itemPerSlot = 1;
            eggplant.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = cucumber.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = cucumber.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = cucumber.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = tomato.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = tomato.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = tomato.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = eggplant.ShallowCopy();
            slot_incu_7.isSeed = true;

            var slot_incu_8 = new Slot();
            slot_incu_8.id = "[[[Incubator]]] #8";
            slot_incu_8.number = 8;
            slot_incu_8.position = outerarea2;
            slot_incu_8.vegetable = eggplant.ShallowCopy();
            slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = eggplant.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetApril()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = april;

            tomato.growingState = grow;
            tomato.productionQuantity = 0;

            pepper.growingState = grow;
            pepper.productionQuantity = 0;

            eggplant.growingState = grow;
            eggplant.productionQuantity = 0;

            cucumber.growingState = grow;
            cucumber.productionQuantity = 0;

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            strawberry.growingState = grow;
            strawberry.productionQuantity = 0;

            leek.growingState = harvest;
            leek.productionQuantity = 75;

            parsnip.growingState = seed;
            parsnip.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            dwarfBean.growingState = harvest;
            dwarfBean.productionQuantity = 25;

            celery.growingState = seed;
            celery.productionQuantity = 0;

            dwarfPeas.growingState = harvest;
            dwarfPeas.productionQuantity = 25;

            beetroot.growingState = grow;
            beetroot.productionQuantity = 0;

            sweetPotatoes.growingState = seed;
            sweetPotatoes.productionQuantity = 0;

            broadBean.growingState = seed;
            broadBean.productionQuantity = 0;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = celery.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = parsnip.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = beetroot.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = broadBean.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = zucchini.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = seed;
            edibleFlower.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.productionQuantity = 0;

            zucchini.growingState = grow;
            zucchini.productionQuantity = 0;

            melon.growingState = seed;
            melon.productionQuantity = 0;

            waterMelon.growingState = seed;
            waterMelon.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            purslane.growingState = seed;
            purslane.itemPerSlot = 1;
            purslane.productionQuantity = 0;

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.itemPerSlot = 1;
            cucumber.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.itemPerSlot = 1;
            eggplant.productionQuantity = 0;

            waterMelon.growingState = seed;
            waterMelon.itemPerSlot = 1;
            waterMelon.productionQuantity = 0;

            lettuce.growingState = seed;
            lettuce.itemPerSlot = 1;
            lettuce.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = mesclun.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = mesclun.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = mesclun.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = purslane.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = tomato.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = pepper.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = eggplant.ShallowCopy();
            slot_incu_7.isSeed = true;

            var slot_incu_8 = new Slot();
            slot_incu_8.id = "[[[Incubator]]] #8";
            slot_incu_8.number = 8;
            slot_incu_8.position = outerarea2;
            slot_incu_8.vegetable = lettuce.ShallowCopy();
            slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = waterMelon.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetMay()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";


            currentMonth = new ProductionMonth();
            currentMonth.month = may;

            tomato.growingState = grow;
            tomato.productionQuantity = 0;

            pepper.growingState = grow;
            pepper.productionQuantity = 0;

            eggplant.growingState = grow;
            eggplant.productionQuantity = 0;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            strawberry.growingState = harvest;
            strawberry.productionQuantity = 50;

            leek.growingState = harvest;
            leek.productionQuantity = 75;

            parsnip.growingState = grow;
            parsnip.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            dwarfBean.growingState = harvest;
            dwarfBean.productionQuantity = 100;

            celery.growingState = seed;
            celery.productionQuantity = 0;

            dwarfPeas.growingState = harvest;
            dwarfPeas.productionQuantity = 100;

            beetroot.growingState = harvest;
            beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = grow;
            sweetPotatoes.productionQuantity = 0;

            broadBean.growingState = grow;
            broadBean.productionQuantity = 0;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 2000;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = celery.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = parsnip.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = beetroot.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = broadBean.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = zucchini.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            tomato.growingState = grow;
            tomato.productionQuantity = 0;

            eggplant.growingState = grow;
            eggplant.productionQuantity = 0;

            pepper.growingState = grow;
            pepper.productionQuantity = 0;

            cucumber.growingState = grow;
            cucumber.productionQuantity = 0;

            zucchini.growingState = grow;
            zucchini.productionQuantity = 0;

            melon.growingState = grow;
            melon.productionQuantity = 0;

            waterMelon.growingState = grow;
            waterMelon.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            purslane.growingState = seed;
            purslane.itemPerSlot = 1;
            purslane.productionQuantity = 0;

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.itemPerSlot = 1;
            cucumber.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.itemPerSlot = 1;
            eggplant.productionQuantity = 0;

            waterMelon.growingState = seed;
            waterMelon.itemPerSlot = 1;
            waterMelon.productionQuantity = 0;

            broadBean.growingState = seed;
            broadBean.itemPerSlot = 1;
            broadBean.productionQuantity = 0;

            edibleFlower.growingState = seed;
            edibleFlower.itemPerSlot = 5;
            edibleFlower.productionQuantity = 0;

            lamblettuce.growingState = seed;
            lamblettuce.itemPerSlot = 5;
            lamblettuce.productionQuantity = 0;

            lettuce.growingState = seed;
            lettuce.itemPerSlot = 1;
            lettuce.productionQuantity = 0;

            kale.growingState = seed;
            kale.itemPerSlot = 1;
            kale.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = broadBean.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = cabbageRave.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = cabbageRave.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = edibleFlower.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = lettuce.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = lettuce.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = whiteOnion.ShallowCopy();
            slot_incu_7.isSeed = true;

            var slot_incu_8 = new Slot();
            slot_incu_8.id = "[[[Incubator]]] #8";
            slot_incu_8.number = 8;
            slot_incu_8.position = outerarea2;
            slot_incu_8.vegetable = chard.ShallowCopy();
            slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = kale.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetJune()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = june;

            tomato.growingState = harvest;
            tomato.productionQuantity = 250;

            pepper.growingState = harvest;
            pepper.productionQuantity = 150;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 250;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 100;

            strawberry.growingState = harvest;
            strawberry.productionQuantity = 80;

            leek.growingState = harvest;
            leek.productionQuantity = 75;

            parsnip.growingState = grow;
            parsnip.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            carrot.growingState = seed;
            carrot.productionQuantity = 0;

            celery.growingState = seed;
            celery.productionQuantity = 0;

            turnip.growingState = seed;
            turnip.productionQuantity = 0;

            beetroot.growingState = harvest;
            beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = grow;
            sweetPotatoes.productionQuantity = 0;

            broadBean.growingState = harvest;
            broadBean.productionQuantity = 1000;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 2000;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 3000;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = celery.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = parsnip.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = carrot.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = turnip.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = broadBean.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = zucchini.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 100;

            tomato.growingState = harvest;
            tomato.productionQuantity = 100;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 100;

            pepper.growingState = harvest;
            pepper.productionQuantity = 150;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 200;

            melon.growingState = grow;
            melon.productionQuantity = 0;

            waterMelon.growingState = grow;
            waterMelon.productionQuantity = 0;


            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            purslane.growingState = seed;
            purslane.itemPerSlot = 1;
            purslane.productionQuantity = 0;

            parsnip.growingState = seed;
            parsnip.itemPerSlot = 1;
            parsnip.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.itemPerSlot = 1;
            cucumber.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.itemPerSlot = 1;
            eggplant.productionQuantity = 0;

            waterMelon.growingState = seed;
            waterMelon.itemPerSlot = 1;
            waterMelon.productionQuantity = 0;

            broadBean.growingState = seed;
            broadBean.itemPerSlot = 1;
            broadBean.productionQuantity = 0;

            edibleFlower.growingState = seed;
            edibleFlower.itemPerSlot = 5;
            edibleFlower.productionQuantity = 0;

            lamblettuce.growingState = seed;
            lamblettuce.itemPerSlot = 5;
            lamblettuce.productionQuantity = 0;

            lettuce.growingState = seed;
            lettuce.itemPerSlot = 1;
            lettuce.productionQuantity = 0;

            kale.growingState = seed;
            kale.itemPerSlot = 1;
            kale.productionQuantity = 0;

            beetroot.growingState = seed;
            beetroot.itemPerSlot = 1;
            beetroot.productionQuantity = 0;

            cabbageBruxelles.growingState = seed;
            cabbageBruxelles.itemPerSlot = 1;
            cabbageBruxelles.productionQuantity = 0;

            turnip.growingState = seed;
            turnip.itemPerSlot = 1;
            turnip.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 1;
            carrot.productionQuantity = 0;

            var slot_incu_1 = new Slot();
            slot_incu_1.id = "[[[Incubator]]] #1";
            slot_incu_1.number = 8;
            slot_incu_1.position = outerarea1;
            slot_incu_1.vegetable = beetroot.ShallowCopy();
            slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = cabbageBruxelles.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = chineseCabbage.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = turnip.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = carrot.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = lettuce.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = whiteOnion.ShallowCopy();
            slot_incu_7.isSeed = true;

            var slot_incu_8 = new Slot();
            slot_incu_8.id = "[[[Incubator]]] #8";
            slot_incu_8.number = 8;
            slot_incu_8.position = outerarea2;
            slot_incu_8.vegetable = chard.ShallowCopy();
            slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = kale.ShallowCopy();
            slot_incu_9.isSeed = true;

            incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetJuly()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType, incubatorProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();
            var incubSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            incubatorProductionType = new ProductionType();
            incubatorProductionType.name = "[[[Incubator]]]";

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = july;

            tomato.growingState = harvest;
            tomato.productionQuantity = 1500;

            pepper.growingState = harvest;
            pepper.productionQuantity = 500;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 1500;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 50;

            strawberry.growingState = harvest;
            strawberry.productionQuantity = 50;

            //leek.growingState = harvest;
            //leek.productionQuantity = 75;

            parsnip.growingState = grow;
            parsnip.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            carrot.growingState = grow;
            carrot.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.productionQuantity = 0;

            celery.growingState = grow;
            celery.productionQuantity = 0;

            turnip.growingState = grow;
            turnip.productionQuantity = 0;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = grow;
            sweetPotatoes.productionQuantity = 0;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            cabbageBruxelles.growingState = seed;
            cabbageBruxelles.productionQuantity = 0;

            dwarfBean.growingState = seed;
            broadBean.productionQuantity = 0;

            dwarfPeas.growingState = seed;
            broadBean.productionQuantity = 0;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 3000;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = blackRadish.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = celery.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = parsnip.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = carrot.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = turnip.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = dwarfPeas.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = zucchini.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 300;

            tomato.growingState = harvest;
            tomato.productionQuantity = 200;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 200;

            pepper.growingState = harvest;
            pepper.productionQuantity = 200;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 200;

            melon.growingState = harvest;
            melon.productionQuantity = 250;

            waterMelon.growingState = grow;
            waterMelon.productionQuantity = 0;


            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            //INCUBATOR SLOTS

            purslane.growingState = seed;
            purslane.itemPerSlot = 1;
            purslane.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.itemPerSlot = 7;
            mesclun.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.itemPerSlot = 1;
            blackRadish.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 2;
            carrot.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.itemPerSlot = 1;
            fennel.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.itemPerSlot = 1;
            pepper.productionQuantity = 0;

            cabbageRave.growingState = seed;
            cabbageRave.itemPerSlot = 1;
            cabbageRave.productionQuantity = 0;

            chineseCabbage.growingState = seed;
            chineseCabbage.itemPerSlot = 1;
            chineseCabbage.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.itemPerSlot = 1;
            cabbageCabu.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.itemPerSlot = 5;
            whiteOnion.productionQuantity = 0;

            chard.growingState = seed;
            chard.itemPerSlot = 5;
            chard.productionQuantity = 0;

            cucumber.growingState = seed;
            cucumber.itemPerSlot = 1;
            cucumber.productionQuantity = 0;

            tomato.growingState = seed;
            tomato.itemPerSlot = 1;
            tomato.productionQuantity = 0;

            eggplant.growingState = seed;
            eggplant.itemPerSlot = 1;
            eggplant.productionQuantity = 0;

            waterMelon.growingState = seed;
            waterMelon.itemPerSlot = 1;
            waterMelon.productionQuantity = 0;

            broadBean.growingState = seed;
            broadBean.itemPerSlot = 1;
            broadBean.productionQuantity = 0;

            edibleFlower.growingState = seed;
            edibleFlower.itemPerSlot = 5;
            edibleFlower.productionQuantity = 0;

            lamblettuce.growingState = seed;
            lamblettuce.itemPerSlot = 5;
            lamblettuce.productionQuantity = 0;

            lettuce.growingState = seed;
            lettuce.itemPerSlot = 1;
            lettuce.productionQuantity = 0;

            kale.growingState = seed;
            kale.itemPerSlot = 1;
            kale.productionQuantity = 0;

            beetroot.growingState = seed;
            beetroot.itemPerSlot = 1;
            beetroot.productionQuantity = 0;

            cabbageBruxelles.growingState = seed;
            cabbageBruxelles.itemPerSlot = 1;
            cabbageBruxelles.productionQuantity = 0;

            turnip.growingState = seed;
            turnip.itemPerSlot = 1;
            turnip.productionQuantity = 0;

            carrot.growingState = seed;
            carrot.itemPerSlot = 1;
            carrot.productionQuantity = 0;

            leek.growingState = seed;
            leek.itemPerSlot = 1;
            leek.productionQuantity = 0;

            //var slot_incu_1 = new Slot();
            //slot_incu_1.id = "[[[Incubator]]] #1";
            //slot_incu_1.number = 8;
            //slot_incu_1.position = outerarea1;
            //slot_incu_1.vegetable = beetroot.ShallowCopy();
            //slot_incu_1.isSeed = true;

            var slot_incu_2 = new Slot();
            slot_incu_2.id = "[[[Incubator]]] #2";
            slot_incu_2.number = 8;
            slot_incu_2.position = outerarea1;
            slot_incu_2.vegetable = cabbageBruxelles.ShallowCopy();
            slot_incu_2.isSeed = true;

            var slot_incu_3 = new Slot();
            slot_incu_3.id = "[[[Incubator]]] #3";
            slot_incu_3.number = 8;
            slot_incu_3.position = outerarea1;
            slot_incu_3.vegetable = chineseCabbage.ShallowCopy();
            slot_incu_3.isSeed = true;

            var slot_incu_4 = new Slot();
            slot_incu_4.id = "[[[Incubator]]] #4";
            slot_incu_4.number = 8;
            slot_incu_4.position = centerarea;
            slot_incu_4.vegetable = blackRadish.ShallowCopy();
            slot_incu_4.isSeed = true;

            var slot_incu_5 = new Slot();
            slot_incu_5.id = "[[[Incubator]]] #5";
            slot_incu_5.number = 8;
            slot_incu_5.position = centerarea;
            slot_incu_5.vegetable = leek.ShallowCopy();
            slot_incu_5.isSeed = true;

            var slot_incu_6 = new Slot();
            slot_incu_6.id = "[[[Incubator]]] #6";
            slot_incu_6.number = 8;
            slot_incu_6.position = centerarea;
            slot_incu_6.vegetable = mesclun.ShallowCopy();
            slot_incu_6.isSeed = true;

            var slot_incu_7 = new Slot();
            slot_incu_7.id = "[[[Incubator]]] #7";
            slot_incu_7.number = 8;
            slot_incu_7.position = outerarea2;
            slot_incu_7.vegetable = purslane.ShallowCopy();
            slot_incu_7.isSeed = true;

            //var slot_incu_8 = new Slot();
            //slot_incu_8.id = "[[[Incubator]]] #8";
            //slot_incu_8.number = 8;
            //slot_incu_8.position = outerarea2;
            //slot_incu_8.vegetable = chard.ShallowCopy();
            //slot_incu_8.isSeed = true;

            var slot_incu_9 = new Slot();
            slot_incu_9.id = "[[[Incubator]]] #9";
            slot_incu_9.number = 8;
            slot_incu_9.position = outerarea2;
            slot_incu_9.vegetable = kale.ShallowCopy();
            slot_incu_9.isSeed = true;

            //incubSlots.Add(slot_incu_1);
            incubSlots.Add(slot_incu_2);
            incubSlots.Add(slot_incu_3);
            incubSlots.Add(slot_incu_4);
            incubSlots.Add(slot_incu_5);
            incubSlots.Add(slot_incu_6);
            incubSlots.Add(slot_incu_7);
            //incubSlots.Add(slot_incu_8);
            incubSlots.Add(slot_incu_9);

            incubatorProductionType.slots = incubSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            productionTypes.Add(incubatorProductionType);

            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetAugust()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = august;

            tomato.growingState = harvest;
            tomato.productionQuantity = 1000;

            pepper.growingState = harvest;
            pepper.productionQuantity = 400;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 1000;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 150;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 200;

            strawberry.growingState = harvest;
            strawberry.productionQuantity = 10;

            //leek.growingState = harvest;
            //leek.productionQuantity = 75;

            blackRadish.growingState = seed;
            blackRadish.productionQuantity = 0;

            parsnip.growingState = harvest;
            parsnip.productionQuantity = 250;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            carrot.growingState = grow;
            carrot.productionQuantity = 0;

            celery.growingState = harvest;
            celery.productionQuantity = 1000;

            turnip.growingState = grow;
            turnip.productionQuantity = 0;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = grow;
            sweetPotatoes.productionQuantity = 0;

            cabbageBruxelles.growingState = grow;
            cabbageBruxelles.productionQuantity = 0;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            dwarfBean.growingState = harvest;
            broadBean.productionQuantity = 250;

            dwarfPeas.growingState = harvest;
            broadBean.productionQuantity = 250;

            //zucchini.growingState = harvest;
            //zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 2500;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = blackRadish.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = celery.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = parsnip.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = carrot.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = turnip.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = dwarfPeas.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 200;

            tomato.growingState = harvest;
            tomato.productionQuantity = 200;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 200;

            pepper.growingState = harvest;
            pepper.productionQuantity = 150;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 150;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 200;

            melon.growingState = harvest;
            melon.productionQuantity = 200;

            waterMelon.growingState = harvest;
            waterMelon.productionQuantity = 450;


            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetSeptember()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = september;

            tomato.growingState = harvest;
            tomato.productionQuantity = 1500;

            pepper.growingState = harvest;
            pepper.productionQuantity = 500;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 1500;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 300;

            strawberry.growingState = harvest;
            strawberry.productionQuantity = 90;

            leek.growingState = seed;
            leek.productionQuantity = 0;

            parsnip.growingState = harvest;
            parsnip.productionQuantity = 250;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            carrot.growingState = harvest;
            carrot.productionQuantity = 100;

            //celery.growingState = harvest;
            //celery.productionQuantity = 1000;

            turnip.growingState = grow;
            turnip.productionQuantity = 0;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = grow;
            sweetPotatoes.productionQuantity = 0;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            cabbageBruxelles.growingState = grow;
            cabbageBruxelles.productionQuantity = 0;

            dwarfBean.growingState = harvest;
            broadBean.productionQuantity = 1000;

            dwarfPeas.growingState = harvest;
            broadBean.productionQuantity = 1000;

            //zucchini.growingState = harvest;
            //zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = tomato.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = pepper.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = eggplant.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = cucumber.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = edibleFlower.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = strawberry.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = strawberry.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 2500;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = carrot.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = turnip.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = tomato.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = dwarfBean.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = dwarfPeas.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 300;

            tomato.growingState = harvest;
            tomato.productionQuantity = 150;

            eggplant.growingState = harvest;
            eggplant.productionQuantity = 150;

            pepper.growingState = harvest;
            pepper.productionQuantity = 250;

            cucumber.growingState = harvest;
            cucumber.productionQuantity = 250;

            zucchini.growingState = harvest;
            zucchini.productionQuantity = 200;

            melon.growingState = harvest;
            melon.productionQuantity = 100;

            waterMelon.growingState = harvest;
            waterMelon.productionQuantity = 150;


            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = edibleFlower;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = tomato;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = tomato;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = eggplant;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = eggplant;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = pepper;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = cucumber;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = zucchini;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = zucchini;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = melon;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = waterMelon;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetOctober()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = october;

            lettuce.growingState = seed;
            lettuce.productionQuantity = 0;

            kale.growingState = seed;
            kale.productionQuantity = 0;

            mesclun.growingState = seed;
            mesclun.productionQuantity = 0;

            pepper.growingState = seed;
            pepper.productionQuantity = 0;

            //edibleFlower.growingState = harvest;
            //edibleFlower.productionQuantity = 300;

            //strawberry.growingState = harvest;
            //strawberry.productionQuantity = 90;

            leek.growingState = grow;
            leek.productionQuantity = 0;

            cabbageCabu.growingState = seed;
            cabbageCabu.productionQuantity = 0;

            blackRadish.growingState = seed;
            blackRadish.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            carrot.growingState = harvest;
            carrot.productionQuantity = 120;

            //celery.growingState = harvest;
            //celery.productionQuantity = 1000;

            turnip.growingState = grow;
            turnip.productionQuantity = 150;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = harvest;
            sweetPotatoes.productionQuantity = 1500;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            cabbageBruxelles.growingState = grow;
            cabbageBruxelles.productionQuantity = 0;

            whiteOnion.growingState = seed;
            whiteOnion.productionQuantity = 0;

            purslane.growingState = seed;
            purslane.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.productionQuantity = 0;

            //zucchini.growingState = harvest;
            //zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = lettuce.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = lettuce.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = lettuce.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = kale.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = mesclun.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = pepper.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = kale.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 2500;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = cabbageCabu.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = blackRadish.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = spinach.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = argulaSalad.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = purslane.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = seed;
            lettuce.productionQuantity = 0;

            kale.growingState = seed;
            kale.productionQuantity = 0;

            //turnip.growingState = seed;
            //turnip.productionQuantity = 0;

            spinach.growingState = seed;
            spinach.productionQuantity = 0;

            //cucumber.growingState = harvest;
            //cucumber.productionQuantity = 250;

            edibleFlower.growingState = seed;
            edibleFlower.productionQuantity = 0;

            fennel.growingState = seed;
            fennel.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce;
            slot_aero_1.isSeed = false;
            slot_aero_1.oneCropPerSlot = true;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = lettuce;
            slot_aero_2.isSeed = false;
            slot_aero_2.oneCropPerSlot = true;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = kale;
            slot_aero_3.isSeed = false;
            slot_aero_3.oneCropPerSlot = true;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = kale;
            slot_aero_4.isSeed = false;
            slot_aero_4.oneCropPerSlot = true;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = fennel;
            slot_aero_5.isSeed = false;
            slot_aero_5.oneCropPerSlot = true;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = spinach;
            slot_aero_6.isSeed = false;
            slot_aero_6.oneCropPerSlot = true;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = spinach;
            slot_aero_7.isSeed = false;
            slot_aero_7.oneCropPerSlot = true;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = spinach;
            slot_aero_8.isSeed = false;
            slot_aero_8.oneCropPerSlot = true;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = edibleFlower;
            slot_aero_9.isSeed = false;
            slot_aero_9.oneCropPerSlot = true;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = fennel;
            slot_aero_10.isSeed = false;
            slot_aero_10.oneCropPerSlot = true;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = lettuce;
            slot_aero_11.isSeed = false;
            slot_aero_11.oneCropPerSlot = true;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetNovember()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = november;

            lettuce.growingState = grow;
            lettuce.productionQuantity = 0;

            kale.growingState = grow;
            kale.productionQuantity = 0;

            mesclun.growingState = grow;
            mesclun.productionQuantity = 0;

            pepper.growingState = harvest;
            pepper.productionQuantity = 180;

            //edibleFlower.growingState = harvest;
            //edibleFlower.productionQuantity = 300;

            //strawberry.growingState = harvest;
            //strawberry.productionQuantity = 90;

            leek.growingState = grow;
            leek.productionQuantity = 0;

            cabbageCabu.growingState = grow;
            cabbageCabu.productionQuantity = 0;

            blackRadish.growingState = grow;
            blackRadish.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            spinach.growingState = seed;
            spinach.productionQuantity = 0;

            cauliflower.growingState = seed;
            cauliflower.productionQuantity = 0;

            //celery.growingState = harvest;
            //celery.productionQuantity = 1000;

            turnip.growingState = grow;
            turnip.productionQuantity = 150;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = harvest;
            sweetPotatoes.productionQuantity = 1500;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            cabbageBruxelles.growingState = grow;
            cabbageBruxelles.productionQuantity = 0;

            whiteOnion.growingState = grow;
            whiteOnion.productionQuantity = 0;

            purslane.growingState = grow;
            purslane.productionQuantity = 0;

            fennel.growingState = grow;
            fennel.productionQuantity = 0;

            //zucchini.growingState = harvest;
            //zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = lettuce.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = lettuce.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = lettuce.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = kale.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = mesclun.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = pepper.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = kale.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 2500;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = cabbageCabu.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = blackRadish.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = spinach.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = argulaSalad.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = purslane.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = grow;
            lettuce.productionQuantity = 0;

            kale.growingState = grow;
            kale.productionQuantity = 0;

            //turnip.growingState = seed;
            //turnip.productionQuantity = 0;

            spinach.growingState = grow;
            spinach.productionQuantity = 0;

            //cucumber.growingState = harvest;
            //cucumber.productionQuantity = 250;

            edibleFlower.growingState = grow;
            edibleFlower.productionQuantity = 0;

            fennel.growingState = grow;
            fennel.productionQuantity = 0;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = lettuce;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = kale;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = kale;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = fennel;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = spinach;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = spinach;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = spinach;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = edibleFlower;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = fennel;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = lettuce;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

        private static ProductionMonth GetDecember()
        {
            List<ProductionArea> productionAreas;
            List<ProductionType> productionTypes;

            ProductionArea indoorProductionArea;
            ProductionType aquaponicProductionType, permacultureProductionType, aerospringProductionType;
            ProductionMonth currentMonth;

            productionAreas = new List<ProductionArea>();
            productionTypes = new List<ProductionType>();

            var aquaponicSlots = new List<Slot>();
            var permaSlots = new List<Slot>();
            var aeroSlots = new List<Slot>();

            indoorProductionArea = new ProductionArea();
            indoorProductionArea.name = "[[[Indoor]]]";

            aquaponicProductionType = new ProductionType();
            aquaponicProductionType.name = "[[[Aquaponics]]]";

            permacultureProductionType = new ProductionType();
            permacultureProductionType.name = "[[[Perma Beds]]]";

            aerospringProductionType = new ProductionType();
            aerospringProductionType.name = "[[[Aerospring]]]";

            currentMonth = new ProductionMonth();
            currentMonth.month = december;

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 180;

            kale.growingState = harvest;
            kale.productionQuantity = 180;

            mesclun.growingState = harvest;
            mesclun.productionQuantity = 180;

            pepper.growingState = harvest;
            pepper.productionQuantity = 180;

            //edibleFlower.growingState = harvest;
            //edibleFlower.productionQuantity = 300;

            //strawberry.growingState = harvest;
            //strawberry.productionQuantity = 90;

            leek.growingState = grow;
            leek.productionQuantity = 0;

            cabbageCabu.growingState = grow;
            cabbageCabu.productionQuantity = 0;

            blackRadish.growingState = grow;
            blackRadish.productionQuantity = 0;

            //chard.growingState = harvest;
            //chard.productionQuantity = 200;

            //carrot.growingState = harvest;
            //carrot.productionQuantity = 120;

            //celery.growingState = harvest;
            //celery.productionQuantity = 1000;

            turnip.growingState = grow;
            turnip.productionQuantity = 150;

            //beetroot.growingState = harvest;
            //beetroot.productionQuantity = 150;

            sweetPotatoes.growingState = harvest;
            sweetPotatoes.productionQuantity = 1500;

            //broadBean.growingState = harvest;
            //broadBean.productionQuantity = 1000;

            cabbageBruxelles.growingState = harvest;
            cabbageBruxelles.productionQuantity = 500;

            whiteOnion.growingState = harvest;
            whiteOnion.productionQuantity = 50;

            purslane.growingState = harvest;
            purslane.productionQuantity = 200;

            fennel.growingState = grow;
            fennel.productionQuantity = 0;

            //zucchini.growingState = harvest;
            //zucchini.productionQuantity = 3500;

            //AQUAPONICS SLOTS

            var slot_aqua_1 = new Slot();
            slot_aqua_1.id = "[[[Zipgrow Tower]]] #1";
            slot_aqua_1.number = 6;
            slot_aqua_1.position = south;
            slot_aqua_1.vegetable = lettuce.ShallowCopy();

            var slot_aqua_2 = new Slot();
            slot_aqua_2.id = "[[[Zipgrow Tower]]] #2";
            slot_aqua_2.number = 3;
            slot_aqua_2.position = south;
            slot_aqua_2.vegetable = lettuce.ShallowCopy();

            var slot_aqua_3 = new Slot();
            slot_aqua_3.id = "[[[Zipgrow Tower]]] #3";
            slot_aqua_3.number = 3;
            slot_aqua_3.position = north;
            slot_aqua_3.vegetable = lettuce.ShallowCopy();

            var slot_aqua_4 = new Slot();
            slot_aqua_4.id = "[[[Zipgrow Tower]]] #4";
            slot_aqua_4.number = 3;
            slot_aqua_4.position = north;
            slot_aqua_4.vegetable = kale.ShallowCopy();

            var slot_aqua_5 = new Slot();
            slot_aqua_5.id = "[[[Zipgrow Tower]]] #5";
            slot_aqua_5.number = 3;
            slot_aqua_5.position = north;
            slot_aqua_5.vegetable = mesclun.ShallowCopy();

            var slot_aqua_6 = new Slot();
            slot_aqua_6.id = "[[[Zipgrow Tower]]] #6";
            slot_aqua_6.number = 3;
            slot_aqua_6.position = north;
            slot_aqua_6.vegetable = pepper.ShallowCopy();

            var slot_aqua_7 = new Slot();
            slot_aqua_7.id = "[[[Zipgrow Tower]]] #7";
            slot_aqua_7.number = 3;
            slot_aqua_7.position = north;
            slot_aqua_7.vegetable = kale.ShallowCopy();

            aquaponicSlots.Add(slot_aqua_1);
            aquaponicSlots.Add(slot_aqua_2);
            aquaponicSlots.Add(slot_aqua_3);
            aquaponicSlots.Add(slot_aqua_4);
            aquaponicSlots.Add(slot_aqua_5);
            aquaponicSlots.Add(slot_aqua_6);
            aquaponicSlots.Add(slot_aqua_7);
            aquaponicProductionType.slots = aquaponicSlots;

            //PERMA SLOTS

            tomato.growingState = harvest;
            tomato.productionQuantity = 2500;

            var slot_perma_1 = new Slot();
            slot_perma_1.id = "[[[Perma Bed]]] #1 [[[Slot]]] #1";
            slot_perma_1.number = 1;
            slot_perma_1.position = top;
            slot_perma_1.vegetable = leek.ShallowCopy();

            var slot_perma_2 = new Slot();
            slot_perma_2.id = "[[[Perma Bed]]] #1 [[[Slot]]] #2";
            slot_perma_2.number = 1;
            slot_perma_2.position = medium;
            slot_perma_2.vegetable = cabbageCabu.ShallowCopy();

            var slot_perma_3 = new Slot();
            slot_perma_3.id = "[[[Perma Bed]]] #1 [[[Slot]]] #3";
            slot_perma_3.number = 1;
            slot_perma_3.position = down;
            slot_perma_3.vegetable = blackRadish.ShallowCopy();

            var slot_perma_4 = new Slot();
            slot_perma_4.id = "[[[Perma Bed]]] #2 [[[Slot]]] #1";
            slot_perma_4.number = 1;
            slot_perma_4.position = top;
            slot_perma_4.vegetable = spinach.ShallowCopy();

            var slot_perma_5 = new Slot();
            slot_perma_5.id = "[[[Perma Bed]]] #2 [[[Slot]]] #2";
            slot_perma_5.number = 1;
            slot_perma_5.position = medium;
            slot_perma_5.vegetable = argulaSalad.ShallowCopy();

            var slot_perma_6 = new Slot();
            slot_perma_6.id = "[[[Perma Bed]]] #2 [[[Slot]]] #3";
            slot_perma_6.number = 1;
            slot_perma_6.position = down;
            slot_perma_6.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_7 = new Slot();
            slot_perma_7.id = "[[[Perma Bed]]] #3 [[[Slot]]] #1";
            slot_perma_7.number = 1;
            slot_perma_7.position = top;
            slot_perma_7.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_8 = new Slot();
            slot_perma_8.id = "[[[Perma Bed]]] #3 [[[Slot]]] #2";
            slot_perma_8.number = 1;
            slot_perma_8.position = medium;
            slot_perma_8.vegetable = sweetPotatoes.ShallowCopy();

            var slot_perma_9 = new Slot();
            slot_perma_9.id = "[[[Perma Bed]]] #3 [[[Slot]]] #3";
            slot_perma_9.number = 1;
            slot_perma_9.position = down;
            slot_perma_9.vegetable = ramialChippedWood.ShallowCopy();

            var slot_perma_10 = new Slot();
            slot_perma_10.id = "[[[Perma Bed]]] #4 [[[Slot]]] #1";
            slot_perma_10.number = 1;
            slot_perma_10.position = top;
            slot_perma_10.vegetable = cabbageBruxelles.ShallowCopy();

            var slot_perma_11 = new Slot();
            slot_perma_11.id = "[[[Perma Bed]]] #4 [[[Slot]]] #2";
            slot_perma_11.number = 1;
            slot_perma_11.position = medium;
            slot_perma_11.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_12 = new Slot();
            slot_perma_12.id = "[[[Perma Bed]]] #4 [[[Slot]]] #3";
            slot_perma_12.number = 1;
            slot_perma_12.position = down;
            slot_perma_12.vegetable = whiteOnion.ShallowCopy();

            var slot_perma_13 = new Slot();
            slot_perma_13.id = "[[[Perma Bed]]] #5 [[[Slot]]] #1";
            slot_perma_13.number = 1;
            slot_perma_13.position = top;
            slot_perma_13.vegetable = purslane.ShallowCopy();

            var slot_perma_14 = new Slot();
            slot_perma_14.id = "[[[Perma Bed]]] #5 [[[Slot]]] #2";
            slot_perma_14.number = 1;
            slot_perma_14.position = medium;
            slot_perma_14.vegetable = fennel.ShallowCopy();

            var slot_perma_15 = new Slot();
            slot_perma_15.id = "[[[Perma Bed]]] #5 [[[Slot]]] #3";
            slot_perma_15.number = 1;
            slot_perma_15.position = down;
            slot_perma_15.vegetable = ramialChippedWood.ShallowCopy();

            permaSlots.Add(slot_perma_1);
            permaSlots.Add(slot_perma_2);
            permaSlots.Add(slot_perma_3);
            permaSlots.Add(slot_perma_4);
            permaSlots.Add(slot_perma_5);
            permaSlots.Add(slot_perma_6);
            permaSlots.Add(slot_perma_7);
            permaSlots.Add(slot_perma_8);
            permaSlots.Add(slot_perma_9);
            permaSlots.Add(slot_perma_10);
            permaSlots.Add(slot_perma_11);
            permaSlots.Add(slot_perma_12);
            permaSlots.Add(slot_perma_13);
            permaSlots.Add(slot_perma_14);
            permaSlots.Add(slot_perma_15);
            permacultureProductionType.slots = permaSlots;

            //AEROSPRING SLOTS

            lettuce.growingState = harvest;
            lettuce.productionQuantity = 150;

            kale.growingState = harvest;
            kale.productionQuantity = 150;

            //turnip.growingState = seed;
            //turnip.productionQuantity = 0;

            spinach.growingState = harvest;
            spinach.productionQuantity = 180;

            //cucumber.growingState = harvest;
            //cucumber.productionQuantity = 250;

            edibleFlower.growingState = harvest;
            edibleFlower.productionQuantity = 100;

            fennel.growingState = harvest;
            fennel.productionQuantity = 150;

            var slot_aero_1 = new Slot();
            slot_aero_1.id = "[[[Aerospring]]] #1";
            slot_aero_1.number = 12;
            slot_aero_1.position = down;
            slot_aero_1.vegetable = lettuce;
            slot_aero_1.isSeed = false;

            var slot_aero_2 = new Slot();
            slot_aero_2.id = "[[[Aerospring]]] #2";
            slot_aero_2.number = 4;
            slot_aero_2.position = medium;
            slot_aero_2.vegetable = lettuce;
            slot_aero_2.isSeed = false;

            var slot_aero_3 = new Slot();
            slot_aero_3.id = "[[[Aerospring]]] #3";
            slot_aero_3.number = 4;
            slot_aero_3.position = medium;
            slot_aero_3.vegetable = kale;
            slot_aero_3.isSeed = false;

            var slot_aero_4 = new Slot();
            slot_aero_4.id = "[[[Aerospring]]] #4";
            slot_aero_4.number = 2;
            slot_aero_4.position = medium;
            slot_aero_4.vegetable = kale;
            slot_aero_4.isSeed = false;

            var slot_aero_5 = new Slot();
            slot_aero_5.id = "[[[Aerospring]]] #5";
            slot_aero_5.number = 2;
            slot_aero_5.position = medium;
            slot_aero_5.vegetable = fennel;
            slot_aero_5.isSeed = false;

            var slot_aero_6 = new Slot();
            slot_aero_6.id = "[[[Aerospring]]] #6";
            slot_aero_6.number = 2;
            slot_aero_6.position = top;
            slot_aero_6.vegetable = spinach;
            slot_aero_6.isSeed = false;

            var slot_aero_7 = new Slot();
            slot_aero_7.id = "[[[Aerospring]]] #7";
            slot_aero_7.number = 2;
            slot_aero_7.position = top;
            slot_aero_7.vegetable = spinach;
            slot_aero_7.isSeed = false;

            var slot_aero_8 = new Slot();
            slot_aero_8.id = "[[[Aerospring]]] #8";
            slot_aero_8.number = 2;
            slot_aero_8.position = top;
            slot_aero_8.vegetable = spinach;
            slot_aero_8.isSeed = false;

            var slot_aero_9 = new Slot();
            slot_aero_9.id = "[[[Aerospring]]] #9";
            slot_aero_9.number = 2;
            slot_aero_9.position = top;
            slot_aero_9.vegetable = edibleFlower;
            slot_aero_9.isSeed = false;

            var slot_aero_10 = new Slot();
            slot_aero_10.id = "[[[Aerospring]]] #10";
            slot_aero_10.number = 2;
            slot_aero_10.position = top;
            slot_aero_10.vegetable = fennel;
            slot_aero_10.isSeed = false;

            var slot_aero_11 = new Slot();
            slot_aero_11.id = "[[[Aerospring]]] #11";
            slot_aero_11.number = 2;
            slot_aero_11.position = top;
            slot_aero_11.vegetable = lettuce;
            slot_aero_11.isSeed = false;

            aeroSlots.Add(slot_aero_1);
            aeroSlots.Add(slot_aero_2);
            aeroSlots.Add(slot_aero_3);
            aeroSlots.Add(slot_aero_4);
            aeroSlots.Add(slot_aero_5);
            aeroSlots.Add(slot_aero_6);
            aeroSlots.Add(slot_aero_7);
            aeroSlots.Add(slot_aero_8);
            aeroSlots.Add(slot_aero_9);
            aeroSlots.Add(slot_aero_10);
            aeroSlots.Add(slot_aero_11);
            aerospringProductionType.slots = aeroSlots;

            productionTypes.Add(aquaponicProductionType);
            productionTypes.Add(permacultureProductionType);
            productionTypes.Add(aerospringProductionType);
            indoorProductionArea.productionTypes = productionTypes;

            productionAreas.Add(indoorProductionArea);
            currentMonth.productionAreas = productionAreas;

            return currentMonth;
        }

    }

    public class ProductionMonth
    {
        public Month month;
        public List<ProductionArea> productionAreas;
    }

    public class ProductionArea
    {
        public string name;
        public List<ProductionType> productionTypes;
    }

    public class ProductionType
    {
        public string name;
        public List<Slot> slots;
    }


    public class Slot
    {
        public string id;
        public Position position;
        public int number;
        public Vegetable vegetable;
        public bool isSeed = false;
        public bool oneCropPerSlot = false;
    }

    public class Vegetable
    {
        public string name;
        public string imagePath;
        public int itemPerSlot;
        public GrowingState growingState;
        public decimal productionQuantity;
        public decimal temperatureMin;
        public decimal temperatureMax;

        public Vegetable ShallowCopy()
        {
            return (Vegetable)this.MemberwiseClone();
        }
    }

    public class GrowingState
    {
        public string name;
        public int Id;
    }

    public class Month
    {
        public string name;
        public int Id;
    }

    public class Position
    {
        public string name;
        public int Id;
    }

}
