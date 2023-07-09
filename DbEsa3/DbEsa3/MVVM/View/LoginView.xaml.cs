using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Google.Protobuf.WellKnownTypes;
using Mysqlx.Prepare;
using System.Reflection.PortableExecutable;
using Oracle.ManagedDataAccess.Client;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using System.IO;
using Renci.SshNet.Sftp;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Mysqlx.Crud;
using Mysqlx.Cursor;
using System.Text.RegularExpressions;
using Org.BouncyCastle.Tls.Crypto;
using System.Windows.Controls.Primitives;
using Org.BouncyCastle.Utilities.Collections;

namespace DbEsa3.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            {
                {

                    
                    string password = contentSSHPassword.Password;
                    string username = contentUsername.Text;
                    string dbpassword = contentDBPassword.Password;

                    // SSH-Tunnel connection

                    PasswordAuthenticationMethod method = new(username, password);
                    ConnectionInfo info = new("compute.beuth-hochschule.de", 22, username, method);
                    ForwardedPortLocal port = new("127.0.0.1", 1521, "141.64.89.143", 1521);

                    SshClient client = new SshClient(info);
                    client.Connect();
                    client.AddForwardedPort(port);
                    port.Start();
                    Console.WriteLine("Connection established.");
                    string connectionString = $"Data Source=localhost:1521/rispdb1;User ID={username};Password={dbpassword};";
                    
                    // Oracle connection
                    
                    OracleConnection connection = new OracleConnection(connectionString);
                    connection.Open();

                    // Unsere SQL_Abfragen in Array gespeichert

                    Query[] queries =
                    {
                        //Abfrage_1
                        new Query(
                            
                            "drop table MonatApril",
                            "create TABLE MonatApril (Monat int PRIMARY KEY, AnzahlderAufrufe int)",
                            "INSERT INTO MonatApril(Monat, AnzahlderAufrufe) " +
                                "SELECT EXTRACT(MONTH from querytime) as Monat,count(*) as AnzahlderAufrufe " +
                                "from aoldata.querydata " +
                                "where query like '%immigration law%' " +
                                "and querytime > '01.04.06' and querytime < '01.05.06' " +
                                "GROUP BY EXTRACT(MONTH from querytime)",
                            "select * from MonatApril"
                        ),
                        //Abfrage_2
                        new Query(
                           
                            "drop table Monate",
                            "create TABLE Monate (Monat int PRIMARY KEY, AnzahlDerAnfragen varchar(255))",
                            "INSERT INTO Monate(Monat, AnzahlDerAnfragen) " +
                                "SELECT EXTRACT(MONTH from querytime) as Monat, Count(*) as AnzahlDerAnfragen " +
                                "FROM aoldata.querydata Where query like '%immigration law%' " +
                                "GROUP BY EXTRACT(MONTH from querytime)",
                            "select* from Monate"
                        ),
                        //Abfrage_3
                        new Query(
                            
                            "drop table Wortkombi",
                            "create Table Wortkombi(Wortkombination varchar(255) PRIMARY KEY, Vorkommen int)",
                            "INSERT INTO Wortkombi(Wortkombination, Vorkommen) " +
                                "SELECT query as Wortkombination, COUNT(*) AS Vorkommen " +
                                "FROM aoldata.querydata " +
                                "WHERE LOWER(query) LIKE '%illegal immigration%' " +
                                "GROUP BY query " +
                                "ORDER BY Vorkommen DESC " +
                                "FETCH First 10 Rows only",
                            "select* from Wortkombi"
                        ),
                        //Abfrage_4
                        new Query(

                            "drop table Uhrzeiten",
                            "create TABLE Uhrzeiten(Stunden int PRIMARY KEY, AnzahlderAnfragen int)",
                            "INSERT INTO Uhrzeiten(Stunden, AnzahlderAnfragen) " +
                            "select Stunden, count(*) as AnzahlderAnfragen " +
                            "from ( "+
                                " select query, EXTRACT(Hour from q.Querytime) as Stunden" +
                                " from (" +
                                    " select anonid, Querytime, query" +
                                    " from aoldata.querydata" +
                                    " group by anonid, Querytime, query" +
                                ") q" +
                           ") sub " +
                           "WHERE query LIKE '%immigration law%' " +
                           "group by Stunden " +
                           "order by AnzahlderAnfragen desc",
                           "select* from Uhrzeiten"
                           ),

                        //Abfrage_5
                        new Query(
                            "drop table Tageszeit",
                            "create Table Tageszeit(Zeitintervall varchar(255) PRIMARY KEY, AnzahlDerAnfragen int)",
                            "INSERT INTO Tageszeit(Zeitintervall, AnzahlDerAnfragen) " +
                            "SELECT" +
                            " CASE " +
                            "WHEN Stunden >= 0 AND Stunden < 6 THEN '0-6'" +
                            "WHEN Stunden >= 6 AND Stunden < 12 THEN '6-12' " +
                            "WHEN Stunden >= 12 AND Stunden < 18 THEN '12-18' " +
                            "ELSE '18-0' " +
                            "END AS Zeitintervall, " +
                            "COUNT(*) AS AnzahlDerAnfragen " +
                            "FROM (" +
                            "SELECT query, EXTRACT(Hour FROM q.Querytime) AS Stunden " +
                            "FROM (" +
                            "SELECT anonid, Querytime, query " +
                            "FROM aoldata.querydata " +
                            "GROUP BY anonid, Querytime, query) q) sub " +
                            "WHERE query LIKE '%illegal immigration%' " +
                            "GROUP BY " +
                            "CASE " +
                            "WHEN Stunden >= 0 AND Stunden < 6 THEN '0-6' " +
                            "WHEN Stunden >= 6 AND Stunden < 12 THEN '6-12' " +
                            "WHEN Stunden >= 12 AND Stunden < 18 THEN '12-18' " +
                            "ELSE '18-0' " +
                            "END " +
                            "ORDER BY AnzahlDerAnfragen DESC",
                            "select* from Tageszeit"
                            ),

                        //Abfrage_6
                        new Query(
                            "drop table US_BUNDESSTAATEN",
                            "CREATE TABLE US_Bundesstaaten (Name VARCHAR2(50),Abkuerzung VARCHAR2(2))",
                            "INSERT ALL "+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES('Alabama', 'AL')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Alaska', 'AK')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Arizona', 'AZ')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Arkansas', 'AR')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('California', 'CA')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Colorado', 'CO')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Connecticut', 'CT')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Delaware', 'DE')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Florida', 'FL')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Georgia', 'GA')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Hawaii', 'HI')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Idaho', 'ID')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Illinois', 'IL')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Indiana', 'IN')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Iowa', 'IA')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Kansas', 'KS')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Kentucky', 'KY')"+
                        " INTO US_Bundesstaaten(Name, Abkuerzung)VALUES('Louisiana', 'LA')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Maine', 'ME')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Maryland', 'MD')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Massachusetts', 'MA')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Michigan', 'MI')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Minnesota', 'MN')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Mississippi', 'MS')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Missouri', 'MO')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Montana', 'MT')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Nebraska', 'NE')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Nevada', 'NV')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('New Hampshire', 'NH')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('New Jersey', 'NJ')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('New Mexico', 'NM')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('New York', 'NY')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('North Carolina', 'NC')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('North Dakota', 'ND')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Ohio', 'OH')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Oklahoma', 'OK')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Oregon', 'OR')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Pennsylvania', 'PA')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Rhode Island', 'RI')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('South Carolina', 'SC')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('South Dakota', 'SD')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Tennessee', 'TN')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Texas', 'TX')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Utah', 'UT')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Vermont', 'VT')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Virginia', 'VA')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Washington', 'WA')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('West Virginia', 'WV')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Wisconsin', 'WI')"+
                        " INTO US_Bundesstaaten (Name, Abkuerzung)VALUES ('Wyoming', 'WY')"+
                        " SELECT 1 FROM DUAL",
                            "drop table Immigration_Law_Haeufigkeit",
                            "CREATE TABLE Immigration_Law_Haeufigkeit (Bundesstaat VARCHAR2(50), Anzahl NUMBER)",
                            "INSERT INTO Immigration_Law_Haeufigkeit (Bundesstaat, Anzahl) " +
                            "SELECT b.Name, COUNT(*) " +
                            "FROM AOLDATA.querydata a " +
                            "JOIN US_Bundesstaaten b ON a.query LIKE '%' || LOWER(b.Name) || '%' " +
                            "where query like '%immigration law%' " +
                            "GROUP BY b.Name",
                            "select * from Immigration_Law_Haeufigkeit"
                            ),

                        //Abfrage_7
                        new Query(
                            "drop Table Internetseite",
                            "create TABLE Internetseite(URL varchar(255) PRIMARY KEY, Anzahl_der_Zugriffe int)",
                            "INSERT INTO Internetseite(URL, Anzahl_der_Zugriffe)" +
                            "SELECT CLICKURL AS URL, COUNT(*) AS Anzahl_der_Zugriffe " +
                            "FROM aoldata.querydata " +
                            "WHERE QUERY LIKE '%immigration law%' " +
                            "AND CLICKURL IS NOT NULL " +
                            "GROUP BY CLICKURL " +
                            "ORDER BY Anzahl_der_Zugriffe DESC " +
                            "FETCH FIRST 10 Rows only",
                            "select* from Internetseite"
                            ),

                        //Abfrage_8
                        new Query(
                            "drop table Quellen",
                            "CREATE TABLE Quellen (Quelle VARCHAR(255) PRIMARY KEY, Aufrufzahlen INT)",
                            "INSERT INTO Quellen (Quelle, Aufrufzahlen) " +
                            "SELECT CLICKURL AS Quelle, COUNT(*) AS Aufrufzahlen " +
                            "FROM aoldata.querydata " +
                            "WHERE CLICKURL IN ('http://www.tucson.com','http://www.tucsoncitizen.com','http://www.livejournal.com','http://www.jornada.com.mx','http://www.azcentral.com','http://www.washingtonpost.com','http://www.nytimes.com','http://edition.cnn.com') " +
                            "GROUP BY CLICKURL " +
                            "ORDER BY Aufrufzahlen DESC",
                            "select* from Quellen"
                            ),

                        //Abfrage_9
                        new Query(
                            "SELECT ANONID as Nutzer, COUNT(*) as Aufrufe " +
                            "FROM AOLDATA.querydata " +
                            "WHERE Query = 'illegal immigration' " +
                            "GROUP BY ANONID " +
                            "ORDER BY Aufrufe DESC " +
                            "FETCH FIRST 10 rows only"
                            ),


                        //Abfrage_10
                        new Query(
                            "SELECT " +
                            "CASE " +
                            "WHEN CLICKURL IS NULL THEN 'Keine URL' " +
                            "ELSE 'Mit URL' " +
                            "END AS Suchanfragen_Value, " +
                            "COUNT(*) AS Clickurl_Value " +
                            "FROM AOLDATA.querydata " +
                            "WHERE Query LIKE '%illegal immigration%' " +
                            "GROUP BY CASE " +
                            "WHEN CLICKURL IS NULL THEN 'Keine URL' " +
                            "ELSE 'Mit URL' " +
                            "END"
                            )

                            };


                    // SQL Abfragen in .csv Datei path "C:\Temp" abgespeichert

                    for (int i = 0; i < queries.Length; i++)
                    {


                        DataTable table = queries[i].CreateTable(connection);


                        string fileName = $"Abfrage_{i + 1}.csv";
                        string path = System.IO.Path.Combine(@"C:\Temp", fileName);


                        // Erstellt Header für die csv Dateien (Notwendig für Python Anwendung zum parsen für die charts)

                        using (StreamWriter writer = new StreamWriter(path))
                        {
                            string bla = "";
                            foreach (DataColumn column in table.Columns) bla += column.ColumnName + ", ";
                            bla = bla.Trim(' ', ',');
                            writer.WriteLine( bla );

                            foreach (DataRow row in table.Rows)
                            {
                                string line = string.Join(", ", row.ItemArray);

                                writer.WriteLine(line);
                            }
                        }
                    }
                    connection.Close();

                    

                    //Python Anwendung parser -> charts werden erstellt
                    Process.Start("C:\\Temp\\parser.exe");

                }
            }
        }
    }
}