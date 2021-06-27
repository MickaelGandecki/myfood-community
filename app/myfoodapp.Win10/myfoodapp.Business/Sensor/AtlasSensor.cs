using myfoodapp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;

namespace myfoodapp.Business.Sensor
{
    public class AtlasSensor
    {
        public SerialDevice serialDevice;
        public SensorTypeEnum sensorType;
        public DataWriter dataWriteObject;
        public DataReader dataReaderObject;
    }
}
