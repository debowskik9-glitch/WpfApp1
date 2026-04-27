using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace WpfApp1
{
    public class BmiRepository
    {
        private const string CONNECTION_STRING = "Data Source=bmi_history.db";

        private bool _isInitialized = false;
        public bool Initialized => _isInitialized;

        public BmiRepository() {   }

        public void Init()
        {
            using var connection = new SqliteConnection(CONNECTION_STRING);
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS BmiHistory(
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Date DATE,
                    Weight REAL,
                    Height REAL,
                    Result REAL,
                    Category TEXT
                    );";
                command.ExecuteNonQuery();
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool TrySave(double weight, double height, BMIResult result)
        {
            bool saveState = false;
            using var connection = new SqliteConnection(CONNECTION_STRING);
            try
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO BmiHistory (Date, Weight, Height, Result, Category) VALUES ($date, $w, $h, $r, $c)";
                command.Parameters.AddWithValue("$date", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("$w", weight);
                command.Parameters.AddWithValue("$h", height);
                command.Parameters.AddWithValue("$r", result.Score);
                command.Parameters.AddWithValue("$c", result.Description);
                command.ExecuteNonQuery();
                saveState = true;
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
            return saveState;
        }
    }
}
