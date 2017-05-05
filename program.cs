using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static BE.DteBoleta;

namespace BE
{
    class Program
    {
        static void Main(string[] args)
        {
            //Conexion a DB
            string cadena = "Provider=VFPOLEDB.1; Data Source = C:\\; Extended Properties = dBASE III; User ID=;Password=";
            OleDbConnection con = new OleDbConnection()
            {
                ConnectionString = cadena
            };
            con.Open();
            string consulto = "SELECT * FROM empres; SELECT * FROM CLIENTES";
            OleDbDataAdapter adaptador = new OleDbDataAdapter(consulto, con);
            adaptador.TableMappings.Add("tabla1", "empres");
            adaptador.TableMappings.Add("tabla2", "CLIENTES");

            DataSet ds = new DataSet();
            adaptador.Fill(ds);
            /*
            folio = Convert.ToString(ds.Tables[0].Rows[1]["fanumero"].ToString());
            ciudad = Convert.ToString(ds.Tables[0].Rows[1]["faciucli"].ToString());
            total = Convert.ToString(ds.Tables[0].Rows[1]["fatotal"].ToString());
            fecha = Convert.ToString(ds.Tables[0].Rows[1]["fafecven"].ToString());
            con.Close();
            */
            var doc = new Document()
            {
                Encabezado = new Encabezado()
                {
                    IdDoc = new IdDoc
                    {
                        TipoDTE = 33
                    },
                    Emisor = new Emisor()
                    {
                        RUTEmisor = Convert.ToString(ds.Tables["table"].Rows[0]["emprut"].ToString()) 
                        
        },
                    Receptor = new Receptor
                    {
                        RUTRecep = Convert.ToString(ds.Tables["table1"].Rows[1]["clrut"].ToString()),
                        RznSocRecep = "FirstName LastName",
                        GiroRecep = "Particular",
                        DirRecep = "Santiago",
                        CmnaRecep = "Santiago"
                    },

                },
                Detalle = new Detalle
                {
                    NmbItem = "Reservation payment",
                    QtyItem = 1,
                    PrcItem = 150
                },
                Referencia =
        new Referencia
        {
            TpoDocRef = 813,
            FolioRef = "XXX"
        }

            };
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new
            XmlSerializer(typeof(Document));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(@"C:\sisgen\xml\prueba.xml");
            mySerializer.Serialize(myWriter, doc);
            myWriter.Close();
        }
    }
}
