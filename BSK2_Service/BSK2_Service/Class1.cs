using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel.Web;

namespace BSK2_Service
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        DataTable GetTable(string name);

         [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        bool delete_row(string name_of_table, string name_of_key, string name_of_column);

          [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        DataTable GetSelectiveTable(string name, string column, string value);

          [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        bool update_row(string name_of_table, string name_of_key,string col_key, string val1, string col1, string val2, string col2,
           string val3, string col3, string val4, string col4, string val5, string col5, string val6, string col6, string val7, string col7);

           [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        bool add_row(string name_of_table,string val1, string col1, string val2, string col2,
           string val3, string col3, string val4, string col4, string val5, string col5, string val6, string col6, string val7, string col7, byte[] passWord, string col8);

           [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        [OperationContract]
        bool Login(string login);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        int GetLabel(string login);

        [OperationContract]
        float averageOrderValue();
    }

    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";
        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }
        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
        [DataMember]
        public DataTable MyTable { get; set; }
    }



}
