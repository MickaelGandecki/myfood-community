using myfoodapp.Model;
using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace myfoodapp.Business.Sensor.HumidityTemperature
{
    /// <summary>
    /// HTU21D Digital Relative Humidity sensor with Temperature IC
    /// http://cdn.sparkfun.com/datasheets/BreakoutBoards/HTU21D.pdf
    /// </summary>
    public sealed class HumidityTemperatureManager
    {
        private LogModel logModel = LogModel.GetInstance;

        private static HumidityTemperatureManager instance;

        public static HumidityTemperatureManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HumidityTemperatureManager();
                }
                return instance;
            }
        }

        public bool IsConnected
        {
            get
            {
                return isConnected;
            }

            private set
            {
                isConnected = value;
            }
        }

        private bool isConnected;

        /// <summary>
        /// Event occurs when connection is made
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// Device I2C Bus
        /// </summary>
        private string i2cBusName = "I2C1";

        /// <summary>
        /// Device I2C Address
        /// </summary>
        private const ushort Htu21dI2cAddress = 0x0040;

        /// <summary>
        /// Trigger temperature measurement command
        /// </summary>
        private const byte SampleTemperatureHold = 0xE3;

        /// <summary>
        /// Trigger humidity measurement command
        /// </summary>
        private const byte SampleHumidityHold = 0xE5;

        /// <summary>
        /// I2C Device
        /// </summary>
        private I2cDevice i2c;

        /// <summary>
        /// Constructs Htu21d with I2C bus identified
        /// </summary>
        /// <param name="i2cBusName">
        /// The bus name to provide to the enumerator
        /// </param>
        public HumidityTemperatureManager()
        {
            IsConnected = false;
        }

        public async Task Connect()
        {
            // Acquire the I2C device
            // MSDN I2C Reference: https://msdn.microsoft.com/en-us/library/windows/apps/windows.devices.i2c.aspx
            //
            // Use the I2cDevice device selector to create an advanced query syntax string
            // Use the Windows.Devices.Enumeration.DeviceInformation class to create a collection using the advanced query syntax string
            // Take the device id of the first device in the collection
            string advancedQuerySyntax = I2cDevice.GetDeviceSelector(i2cBusName);
            DeviceInformationCollection deviceInformationCollection = await DeviceInformation.FindAllAsync(advancedQuerySyntax);
            string deviceId = deviceInformationCollection[0].Id;

            // Establish an I2C connection to the HTU21D
            //
            // Instantiate the I2cConnectionSettings using the device address of the HTU21D
            // - Set the I2C bus speed of connection to fast mode
            // - Set the I2C sharing mode of the connection to shared
            //
            // Instantiate the the HTU21D I2C device using the device id and the I2cConnectionSettings
            I2cConnectionSettings htu21dConnection = new I2cConnectionSettings(Htu21dI2cAddress);
            htu21dConnection.BusSpeed = I2cBusSpeed.FastMode;
            htu21dConnection.SharingMode = I2cSharingMode.Shared;

            this.i2c = await I2cDevice.FromIdAsync(deviceId, htu21dConnection);

            // Test to see if the I2C devices are available.
            //
            // If the I2C devices are not available, this is
            // a good indicator the weather shield is either
            // missing or configured incorrectly. Therefore we
            // will disable the weather shield functionality to
            // handle the failure case gracefully. This allows
            // the invoking application to remain deployable
            // across the Universal Windows Platform.
            if (null == this.i2c)
            {
                this.isConnected = false;
            }
            else
            {
                byte[] i2cTemperatureData = new byte[3];

                try
                {
                    this.i2c.WriteRead(new byte[] { SampleTemperatureHold }, i2cTemperatureData);
                    this.isConnected = true;

                    Connected?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    logModel.AppendLog(Log.CreateErrorLog("Exception on Temp/Hum init", ex));
                    this.isConnected = false;
                }
            }
        }

        /// <summary>
        /// Calculates the dew point temperature
        /// </summary>
        public float DewPoint
        {
            get
            {
                if (!this.isConnected)
                {
                    return 0f;
                }

                ushort rawTemperatureData = this.RawTemperature;
                ushort rawHumidityData = this.RawHumidity;

                double temperatureCelsius = ((175.72 * rawTemperatureData) / 65536) - 46.85;
                double humidityRelative = ((125.0 * rawHumidityData) / 65536) - 6.0;

                const double DewConstA = 8.1332;
                const double DewConstB = 1762.39;
                const double DewConstC = 235.66;

                double paritalPressure;
                double dewPoint;

                // To calculate the dew point, the partial pressure must be determined first.
                // See datasheet page 16 for details.
                // Partial pressure = 10 ^ (A - (B / (Temp + C)))
                paritalPressure = DewConstA - (DewConstB / (temperatureCelsius + DewConstC));
                paritalPressure = System.Math.Pow(10, paritalPressure);

                // Dew point is calculated using the partial pressure, humidity and temperature.
                // The datasheet says "Ambient humidity in %RH, computed from HTU21D(F) sensor" on page 16 is doesn't say to use the temperature compensated
                // RH value. Therefore, we use the raw RH value straight from the sensor.
                // Dew point = -(C + B / (log(RH * PartialPress / 100) - A))
                dewPoint = humidityRelative * paritalPressure / 100;
                dewPoint = System.Math.Log10(dewPoint) - DewConstA;
                dewPoint = DewConstB / dewPoint;
                dewPoint = -(dewPoint + DewConstC);

                return Convert.ToSingle(dewPoint);
            }
        }

        public float Humidity
        {
            get
            {
                if (!this.isConnected)
                {
                    return 0f;
                }

                ushort rawHumidityData = this.RawHumidity;
                double humidityRelative = ((125.0 * rawHumidityData) / 65536) - 6.0;

                return Convert.ToSingle(humidityRelative);
            }
        }

        public float Temperature
        {
            get
            {
                if (!this.isConnected)
                {
                    return 0f;
                }

                ushort rawTemperatureData = this.RawTemperature;
                double temperatureCelsius = ((175.72 * rawTemperatureData) / 65536) - 46.85;

                return Convert.ToSingle(temperatureCelsius);
            }
        }

        /// <summary>
        /// Gets the raw humidity value from the IC.
        /// </summary>
        private ushort RawHumidity
        {
            get
            {
                ushort humidity = 0;
                byte[] i2cHumidityData = new byte[3];

                // Request humidity data from the HTU21D
                // HTU21D datasheet: http://dlnmh9ip6v2uc.cloudfront.net/datasheets/BreakoutBoards/HTU21D.pdf
                //
                // Write the SampleHumidityHold command (0xE5) to the HTU21D
                // - HOLD means it will block the I2C line while the HTU21D calculates the humidity value
                //
                // Read the three bytes returned by the HTU21D
                // - byte 0 - MSB of the humidity
                // - byte 1 - LSB of the humidity
                // - byte 2 - CRC
                //
                // NOTE: Holding the line allows for a `WriteRead` style transaction
                this.i2c.WriteRead(new byte[] {SampleHumidityHold }, i2cHumidityData);

                // Reconstruct the result using the first two bytes returned from the device
                //
                // NOTE: Zero out the status bits (bits 0 and 1 of the LSB), but keep them in place
                // - status bit 0 - not assigned
                // - status bit 1
                // -- off = temperature data
                // -- on = humdity data
                humidity = (ushort)(i2cHumidityData[0] << 8);
                humidity |= (ushort)(i2cHumidityData[1] & 0xFC);

                // Test the integrity of the data
                //
                // Ensure the data returned is humidity data (hint: byte 1, bit 1)
                // Test cyclic redundancy check (CRC) byte
                //
                // WARNING: HTU21D firmware error - XOR CRC byte with 0x62 before attempting to validate
                bool humidityData = 0x00 != (0x02 & i2cHumidityData[1]);
                if (!humidityData)
                {
                    logModel.AppendLog(Log.CreateLog(String.Format("Un-consistent Humidity - {0}", humidity), Log.LogType.Warning));
                    return 0;
                }

                bool validData = this.ValidCyclicRedundancyCheck(humidity, (byte)(i2cHumidityData[2] ^ 0x62));
                if (!validData)
                {
                    logModel.AppendLog(Log.CreateLog(String.Format("Un-consistent Humidity - {0}", humidity), Log.LogType.Warning));
                    return 0;
                }

                return humidity;
            }
        }

        /// <summary>
        /// Gets the raw temperature value from the IC.
        /// </summary>
        private ushort RawTemperature
        {
            get
            {
                ushort temperature = 0;
                byte[] i2cTemperatureData = new byte[3];

                // Request temperature data from the HTU21D
                // HTU21D datasheet: http://dlnmh9ip6v2uc.cloudfront.net/datasheets/BreakoutBoards/HTU21D.pdf
                //
                // Write the SampleTemperatureHold command (0xE3) to the HTU21D
                // - HOLD means it will block the I2C line while the HTU21D calculates the temperature value
                //
                // Read the three bytes returned by the HTU21D
                // - byte 0 - MSB of the temperature
                // - byte 1 - LSB of the temperature
                // - byte 2 - CRC
                //
                // NOTE: Holding the line allows for a `WriteRead` style transaction
                this.i2c.WriteRead(new byte[] {SampleTemperatureHold }, i2cTemperatureData);

                // Reconstruct the result using the first two bytes returned from the device
                //
                // NOTE: Zero out the status bits (bits 0 and 1 of the LSB), but keep them in place
                // - status bit 0 - not assigned
                // - status bit 1
                // -- off = temperature data
                // -- on = humdity data
                temperature = (ushort)(i2cTemperatureData[0] << 8);
                temperature |= (ushort)(i2cTemperatureData[1] & 0xFC);

                // Test the integrity of the data
                //
                // Ensure the data returned is temperature data (hint: byte 1, bit 1)
                // Test cyclic redundancy check (CRC) byte
                bool temperatureData = 0x00 == (0x02 & i2cTemperatureData[1]);
                if (!temperatureData)
                {
                    logModel.AppendLog(Log.CreateLog(String.Format("Un-consistent Air Temperature - {0}", temperature), Log.LogType.Warning));
                    return 0;
                }

                bool validData = this.ValidCyclicRedundancyCheck(temperature, i2cTemperatureData[2]);
                if (!validData)
                {
                    logModel.AppendLog(Log.CreateLog(String.Format("Un-consistent Air Temperature - {0}", temperature), Log.LogType.Warning));
                    return 0;
                }

                return temperature;
            }
        }

        private bool ValidCyclicRedundancyCheck(ushort data, byte crc)
        {
            // Validate the 8-bit cyclic redundancy check (CRC) byte
            // CRC: http://en.wikipedia.org/wiki/Cyclic_redundancy_check
            // Generator polynomial x^8 + x^5 + x^4 + 1: 100110001(0x0131)
            const int CrcBitLength = 8;
            const int DataLength = 16;
            const ushort GeneratorPolynomial = 0x0131;

            int crcData = data << CrcBitLength;

            for (int i = DataLength - 1; 0 <= i; --i)
            {
                if (0 == (0x01 & (crcData >> (CrcBitLength + i))))
                {
                    continue;
                }

                crcData ^= GeneratorPolynomial << i;
            }

            return crc == crcData;
        }

        public void Dispose()
        {
            IsConnected = false;
            i2c.Dispose();
        }
    }
}