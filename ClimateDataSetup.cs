using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//References
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;

/// <summary>
/// DATABASE
/// </summary>
public class ClimateDataSetup : MonoBehaviour
{
    enum Screen { MainMenu, CountryStats, ManipulateCountry };
    Screen currentScreen = Screen.MainMenu;

    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;

    //   private Text data_stuff;
    private int countryID;

    private int year = 1960;

    private string AgentName;

   

    // Start is called before the first frame update
    void Start()
    {
        string DatabaseName = "CorruptionData.db";
        string filepath = Application.dataPath + "/database/" + DatabaseName;
        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
        reader_function(countryID);

        ShowMainMenu();

    }

    /// <summary>
    /// Read Country Information from the Database
    /// </summary>


    private void reader_function(int index)
    {
        // int idreaders ;






        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();


            string sqlQuery = "SELECT Cluster.ClusterName, Country.CountryName, Country.CountryInitPop, Country.CountryInitAdults, Country.CountryInitTeens, Country.CountryInitInfant, Country.CountryPopVariance, Country.CountryVehicleVariance, Cluster.LuxuryVariance, Cluster.CurrencyVariance, Cluster.CO2Variance FROM Cluster INNER JOIN Country ON Cluster.[ClusterID] =" + index;

            //fix the query above so its linked to the country ID not cluster ID

            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string  ClusterName = reader.GetString(0);
                string  CountryName = reader.GetString(1);
                string  CountryInitPop = reader.GetString(2);
                float CountryInitAdults = reader.GetFloat(3);
                float CountryInitTeens = reader.GetFloat(4);
                float CountryInitInfant = reader.GetFloat(5);
                float CountryPopVariance = reader.GetFloat(6);


                //   data_stuff.text += CountryName + "\n";
                Debug.Log(" Country name =" + CountryName);
                Debug.Log(" Cluster =" + ClusterName);
                Debug.Log(" CountryInitPop =" + CountryInitPop);
                Debug.Log(" CountryInitAdults =" + CountryInitAdults);


                Terminal.WriteLine("");

                Terminal.WriteLine("=================== | " + year + " | ===================");
                Terminal.WriteLine("Country: " + CountryName);
                Terminal.WriteLine(" ");
                Terminal.WriteLine("Cluster: " + ClusterName);
                Terminal.WriteLine("======================================================");
                Terminal.WriteLine("POPULATION");
                Terminal.WriteLine(" ");
                float CPop = float.Parse(CountryInitPop);
                float Adults = CPop * CountryInitAdults;
                float Teens = CPop * CountryInitTeens;
                float Infants = CPop * CountryInitInfant;
                Terminal.WriteLine("Population: " + CountryInitPop);
                Terminal.WriteLine("Infants Percentage: " + CountryInitInfant + "    Current Population of Infant:" + ((double)Infants).ToString("N0"));
                Terminal.WriteLine("Teen Percentage: " + CountryInitTeens + "        Current Population of Teens:" + ((double)Teens).ToString("N0"));
                Terminal.WriteLine("Adult Percentage: " + CountryInitAdults + "      Current Population of Adults:" + ((double)Adults).ToString("N0"));
                Terminal.WriteLine(" ");
                Terminal.WriteLine("======================================================");
                Terminal.WriteLine("VEHICLE EMISSIONS");
                Terminal.WriteLine(" ");

                // make a new sql to get the information
                //              Terminal.WriteLine("Current Population of Infants:" + Infants);

                Terminal.WriteLine("This country is a " + ClusterName + " level. As such, Vehicle Emissions are affected by " +  CountryPopVariance + "%.");
                // make a new sql to get the information
                //SELECT Country.CountryName, VehicleEmissions.VehicleUseType, VehicleEmissions.VehicleUsePercentage, VehicleEmissions.kmper, MCEF.FuelType, MCEF.Co2EmmFactor, MCEF.Unit FROM MCEF INNER JOIN ((Country INNER JOIN CountryRegion ON Country.[CountryID] = CountryRegion.[CountryID]) INNER JOIN VehicleEmissions ON CountryRegion.[CountryRegionID] = VehicleEmissions.[CountryRegionID]) ON MCEF.[MCEFID] = VehicleEmissions.[MCEFID];
                // make an array getting the information



 //               Terminal.WriteLine("Personal Vehicles Emmission: ");
//               Terminal.WriteLine("Public Vehicles Emmission: ");
 //               Terminal.WriteLine("Large Vehicles Emmission: ");
 //               Terminal.WriteLine("Environmental Friendly Vehicles Emmission: ");
                Terminal.WriteLine("======================================================");
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            //  dbconn = null;

        }
    }
        
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    void ShowMainMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("======================================================");
        Terminal.WriteLine("          CLIMATE CORRUPTION  (Database Test)          ");
        Terminal.WriteLine("======================================================");
        Terminal.WriteLine("You are about to commit CLimate Change Corruption. Before you begin, What is your Agent Code name?");

    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    /// <param name="input"></param>
    /// 


    void OnUserInput(string input)
    {

        if (input == "menu")
        {
            ShowMainMenu(); // Returns to main menu - we can always to direct to main menu
        }
        else if (currentScreen == Screen.CountryStats)
        {
            ChooseCountry();
        }
        else if (currentScreen == Screen.ManipulateCountry)
        {
            Terminal.WriteLine("==========GOT HERE=======================");
            DisplayCountryScreen(input);
        }
        else
        {
            string WelcomeAgent = "Your Evil Agent registrations have been approved Evil Agent " + input + ". We will be manipulating a country.";
            Terminal.WriteLine(WelcomeAgent);
            string AgentName = input;
            ChooseCountry();
        }
    }

        void ChooseCountry ()
    {
        currentScreen = Screen.ManipulateCountry;

        Terminal.WriteLine("\n Choose a Country:");
        Terminal.WriteLine("======================================================");
        Terminal.WriteLine("1. USA");
 //       Terminal.WriteLine("2. Australia");
        Terminal.WriteLine("Type a number Evil Agent " + AgentName);

    }
    void DisplayCountryScreen(string index)
    {
        currentScreen = Screen.CountryStats;
        Terminal.ClearScreen();

        Terminal.WriteLine("Choose a Country:");
        Terminal.WriteLine("          CLIMATE CORRUPTION  (Database Test)          ");
        Terminal.WriteLine("======================================================");
        string CountryChoice = "Showing data for ID number " + index;
        Terminal.WriteLine(CountryChoice);
        int CountryChoiceIndex = int.Parse(index);
        reader_function(CountryChoiceIndex);
    }





}
