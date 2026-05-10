using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace Business_Logic_Layer
{
    public class PeopleServices
    {
        public static DataTable GetAllPeople()
        {
            DataTable dt = PeopleData.GetAllPeople();
            return dt;
        }

        public static DataTable GetAllCountries()
        {

            return PeopleData.GetAllCountries();
        }

        public static bool IsNationalNuExist(string NationalNu)
        {
            if (PeopleData.IsNationalNumberExists(NationalNu))
            {
                return true; 
            }
            else
                return false;

        }

        public static int AddPerson(string nationalNo, string firstName, string secondName, string thirdName,
    string lastName, DateTime dateOfBirth, byte gendor, string address, string phone, string email,
    int nationalityCountryID, string imagePath)
        {
            return PeopleData.AddPerson(nationalNo, firstName, secondName, thirdName, lastName,
                dateOfBirth, gendor, address, phone, email, nationalityCountryID, imagePath);
        }

        public static bool DeletePerson(int PersonID)
        {
            return PeopleData.DeletePerson(PersonID);
        }

        public DataRow GetPersonByPersonID(int PersonID)
        {
            return PeopleData.GetPersonByPersonID(PersonID);
        }

        public static void UpdatePerson(int personId, string nationalNo, string firstName, string secondName,
                          string thirdName, string lastName, DateTime dateOfBirth, byte gendor,
                          string address, string phone, string email, int nationalityCountryID,
                          string imagePath)
        {
            PeopleData data = new PeopleData();
            data.UpdatePerson(personId, nationalNo, firstName, secondName, thirdName,
                              lastName, dateOfBirth, gendor, address, phone, email,
                              nationalityCountryID, imagePath);
        }
        public string GetCountryByID (int CountryID)
        {
            return PeopleData.GetCountryNameByID(CountryID);
        }


    }
}