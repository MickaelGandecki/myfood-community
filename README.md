# myfood app & hub - Solar Smart Greenhouse Using Vertical Aquaponic Towers

__Technical architecture overview:__
<a href="https://myfood.eu/wp-content/uploads/2017/03/myfood-solution-architecture.pdf">download the document</a>

__To compile flawlessly:__ 
- on Visual Studio 2015/2017/2019, under the Package Manager Console, execute Update-Package -Reinstall
- download Telerik ASP MVC component trial to get Kendo.MVC.dll

__To deploy local database and dummy data:__
- on Visual Studio 2015/2017/2019, under the Package Manager Console, execute EntityFramework\Update-Database

__Hardware pre-requisite:__
- Raspberry Pi 3B with Win10 IOT
- RTC Pi Zero (<a href="https://www.abelectronics.co.uk/p/70/RTC-Pi-Zero">see the product</a>)
- Sigfox Extension Board (<a href="https://yadom.fr/carte-rpisigfox.html">see the product</a>)
- Humidity / Air Temperature Sensor (<a href="https://www.adafruit.com/product/1899">see the product</a>)
- Atlas Sensors (pH, Temp, DO, ORP) plus USB Board (<a href="https://www.atlas-scientific.com/product_pages/components/usb-iso.html">see the product</a>) and EZO circuits

__Material pre-requisite:__
- 22/14sq² tempered glass greenhouse (ie: S208 or S106 Model from <a href="http://www.acd.eu/en">ACD</a>) 
- 24X 5' Zipgrow towers (<a href="https://brightagrotech.com">see the product</a>) 
- Wooden structure (<a href="https://myfood.eu/wp-content/uploads/2017/03/myfood-family-plan.pdf">see the plan</a>)
- Water pump (ie: DCS-2000)
- PVC-U tubes
- 2X 600L food contact fish tanks (1150mm X 1550mm X 450mm)

__Greenhouse setup:__
<a href="https://myfood.eu/wp-content/uploads/2017/03/myfood-greenhouse-setup.pdf">see the plan</a>

__OpenData API__

__Get the Production Units list:__

 ```https://hub.myfood.eu/opendata/productionunits/ ```
 
 __Get the Production Units measures (last 1000):__
 
 ```https://hub.myfood.eu/opendata/productionunits/{id}/measures ```
 
 __Full list of Components - Bill of Materials__
 
-	Raspberry Pi 3 Model B 1.00 Unit(s)
-	EZO RTD temperature acquisition card from Atlas Scientific 1.00 Unit(s)
-	EZO PH temperature acquisition card from Atlas Scientific 1.00 Unit(s)
-	Atlas Scientific pH Probe 1.00 Unit(s)
-	Atlas Scientific Water Temperature Sensor (PT1000) 1.00 Unit(s)
-	Atlas Scientific USB isolation board 2.00 Unit(s)
-	Temperature / Humidity Sensor (HTU21D-F I2C) 1.00 Unit(s)
-	Battery CR2032 1.00 Unit(s)
-	Retention pins 0.1 "36 - male right angle - 5 units 2.00 Unit(s)
-	Clock for Raspberry RTC Zero 1.00 Unit(s)
-	F-F Color Cable for Temp / Humidity - 300mm 4.00 Unit(s)
-	Cable 3g 10 cm 1.00 Unit(s)
-	Stainless steel nut (DIN 934) (4mm) 2.00 Unit(s) Labor preparation case 1.00 Unit(s)
-	Component sensors provided by SNOC 1.00 Unit(s)
-	SanDisk Ultra 16GB micro SDHC Memory Card (80MB / s - Class 10 FFP) 1.00 Unit(s) 
-	SigFox communication board kit for Raspberry Pi with DIN enclosure 1.00 Unit(s)
-	Micro USB angled 1.00 Unit(s)
-	Electrical box - 1 row, 6 modules 1.00 Unit(s)
-	Tube for protection (PVC - 32mm diameter - 6cm long) 1.00 Unit(s)
-	Wago (3 x 0.08-4 mm) 2.00 Unit(s)
-	DC-DC Regulator Converter (5-30V to 0.8-28V - 5A) 1.00 Unit(s)
-	TF M4X35 INOX A2 DIN 963 2.00 Unit(s)
-	DC Pump Controller 1.00 Unit(s)
-	Cable Power Connector 1.00 Unit(s)
-	USB joint Iso 2.00 Unit(s)
-	Cable glands 2.00 Unit(s)
