using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Project228
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void AutoFillTable1()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM Doctor";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }

        private void AutoFillTable2()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Hospital", sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            try
            {
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
            }
        }

        private void AutoFillTable3()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM Patient";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView3.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }

        private void AutoFillTable4()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
            string selectQuery = "SELECT * FROM Reception";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectQuery, sqlConnection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    try
                    {
                        sqlConnection.Open();
                        adapter.Fill(dataTable);
                        dataGridView4.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при отриманні даних: {ex.Message}");
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString);

            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open)
                MessageBox.Show("Підключення встановленно");

            dataGridView1.Dock = DockStyle.Fill;

            AutoFillTable1();

            searchCriteriaComboBox.Items.Add("Повне ім'я");
            searchCriteriaComboBox.Items.Add("Спеціалізація");
            searchCriteriaComboBox.Items.Add("ID лікарні");
            searchCriteriaComboBox.SelectedIndex = 0;

            dataGridView2.Dock = DockStyle.Fill;

            AutoFillTable2();

            comboBox1.Items.Add("Назва лікарні");
            comboBox1.Items.Add("Адреса");
            comboBox1.Items.Add("Номер телефону");
            comboBox1.Items.Add("HospitalID");

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            dataGridView3.Dock = DockStyle.Fill;
            dataGridView4.Dock = DockStyle.Fill;

            AutoFillTable3();
            AutoFillTable4();

            comboBox2.Items.Add("Повне ім'я");
            comboBox2.Items.Add("Дата народження");
            comboBox2.Items.Add("Адреса");
            comboBox2.Items.Add("Номер телефону");
            comboBox2.Items.Add("DoctorID");
            comboBox2.Items.Add("PatientID");

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }

            comboBox3.Items.Add("DateTime");
            comboBox3.Items.Add("PatientID");
            comboBox3.Items.Add("DoctorID");
            comboBox3.Items.Add("Diagnosis");
            comboBox3.Items.Add("Treatment");
            comboBox3.Items.Add("Amount");
            comboBox3.Items.Add("AppointmentID");

            if (comboBox3.Items.Count > 0)
            {
                comboBox3.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Doctor] (FullName, Specialization, HospitalID) VALUES (@FullName, @Specialization, @HospitalID)", sqlConnection);

            command.Parameters.AddWithValue("FullName", textBox1.Text);
            command.Parameters.AddWithValue("Specialization", textBox2.Text);
            command.Parameters.AddWithValue("HospitalID", textBox3.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AutoFillTable1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Doctor SET FullName = @FullName, Specialization = @Specialization, HospitalID = @HospitalID WHERE DoctorID = @DoctorID;", sqlConnection);

            command.Parameters.AddWithValue("DoctorID", textBox4.Text);
            command.Parameters.AddWithValue("FullName", textBox5.Text);
            command.Parameters.AddWithValue("Specialization", textBox6.Text);
            command.Parameters.AddWithValue("HospitalID", textBox7.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AutoFillTable1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox8.Text, out int doctorIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deleteReceptionQuery = "DELETE FROM Reception WHERE DoctorID = @DoctorID;";
                        using (SqlCommand deleteReceptionCommand = new SqlCommand(deleteReceptionQuery, sqlConnection))
                        {
                            deleteReceptionCommand.Parameters.AddWithValue("@DoctorID", doctorIdToDelete);
                            deleteReceptionCommand.ExecuteNonQuery();
                        }

                        string deleteDoctorQuery = "DELETE FROM Doctor WHERE DoctorID = @DoctorID;";
                        using (SqlCommand deleteDoctorCommand = new SqlCommand(deleteDoctorQuery, sqlConnection))
                        {
                            deleteDoctorCommand.Parameters.AddWithValue("@DoctorID", doctorIdToDelete);
                            int doctorRowsAffected = deleteDoctorCommand.ExecuteNonQuery();

                            if (doctorRowsAffected > 0)
                            {
                                string maxIdQuery = "SELECT ISNULL(MAX(DoctorID), 0) FROM Doctor;";
                                using (SqlCommand getMaxIdCommand = new SqlCommand(maxIdQuery, sqlConnection))
                                {
                                    int maxId = (int)getMaxIdCommand.ExecuteScalar();

                                    string reseedQuery = $"DBCC CHECKIDENT ('Doctor', RESEED, {maxId});";
                                    using (SqlCommand reseedCommand = new SqlCommand(reseedQuery, sqlConnection))
                                    {
                                        reseedCommand.ExecuteNonQuery();
                                    }
                                }

                                AutoFillTable1();
                                textBox8.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Лікаря з ID " + doctorIdToDelete + " не знайдено.");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show("Не вдалося видалити лікаря. Спочатку видаліть записи про прийом.");
                        }
                        else
                        {
                            MessageBox.Show("Виникла помилка при видаленні: " + ex.Message);
                        }
                    }
                    finally
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                            sqlConnection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            string searchText = textBox9.Text.Trim();
            string selectedCriteria = searchCriteriaComboBox.SelectedItem as string;
            string searchColumn = "";

            if (selectedCriteria == "Повне ім'я")
            {
                searchColumn = "FullName";
            }
            else if (selectedCriteria == "Спеціалізація")
            {
                searchColumn = "Specialization";
            }
            else if (selectedCriteria == "ID лікарні")
            {
                searchColumn = "HospitalID";
            }

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(searchColumn))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
                string searchQuery = $"SELECT * FROM Doctor WHERE {searchColumn} LIKE @SearchTerm";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                        command.Parameters.AddWithValue("@SearchTerm", "%" + searchText + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при пошуку: {ex.Message}");
                    }
                }
            }
            else
            {
                AutoFillTable1();
            }
        }

        private void isOpenSecond(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                dataGridView2.Dock = DockStyle.Fill;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand(
                "INSERT INTO [Hospital] (HospitalName, Addres, PhoneNumber) VALUES (@HospitalName, @Addres, @PhoneNumber)",
                sqlConnection);

            command.Parameters.AddWithValue("@HospitalName", textBox10.Text);
            command.Parameters.AddWithValue("@Addres", textBox11.Text);
            command.Parameters.AddWithValue("@PhoneNumber", textBox12.Text);

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show($"Додано записів: {rowsAffected}");
                AutoFillTable2();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Помилка при додаванні лікарні: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Hospital SET HospitalName = @HospitalName, Addres = @Addres, PhoneNumber = @PhoneNumber WHERE HospitalID = @HospitalID;", sqlConnection);

            command.Parameters.AddWithValue("HospitalID", textBox13.Text);
            command.Parameters.AddWithValue("HospitalName", textBox14.Text);
            command.Parameters.AddWithValue("Addres", textBox15.Text);
            command.Parameters.AddWithValue("PhoneNumber", textBox16.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AutoFillTable2();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox17.Text, out int hospitalIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deleteDoctorsQuery = "DELETE FROM Doctor WHERE HospitalID = @HospitalID;";
                        using (SqlCommand deleteDoctorsCommand = new SqlCommand(deleteDoctorsQuery, sqlConnection))
                        {
                            deleteDoctorsCommand.Parameters.AddWithValue("@HospitalID", hospitalIdToDelete);
                            deleteDoctorsCommand.ExecuteNonQuery();
                        }

                        string deleteHospitalQuery = "DELETE FROM Hospital WHERE HospitalID = @HospitalID;";
                        using (SqlCommand deleteHospitalCommand = new SqlCommand(deleteHospitalQuery, sqlConnection))
                        {
                            deleteHospitalCommand.Parameters.AddWithValue("@HospitalID", hospitalIdToDelete);
                            int rows = deleteHospitalCommand.ExecuteNonQuery();

                            if (rows > 0)
                            {
                                int newIdentitySeed = 0;

                                string getMaxIdQuery = "SELECT ISNULL(MAX(HospitalID), 0) FROM Hospital";
                                using (SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, sqlConnection))
                                {
                                    newIdentitySeed = (int)getMaxIdCommand.ExecuteScalar();
                                }

                                string resetIdentityQuery = $"DBCC CHECKIDENT ('Hospital', RESEED, {newIdentitySeed})";
                                using (SqlCommand resetIdentityCommand = new SqlCommand(resetIdentityQuery, sqlConnection))
                                {
                                    resetIdentityCommand.ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                AutoFillTable2();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        AutoFillTable2();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
                AutoFillTable2();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string searchText = textBox18.Text.Trim();
            string selectedCriteria = comboBox1.SelectedItem as string;
            string searchColumn = "";

            if (selectedCriteria == "Назва лікарні")
            {
                searchColumn = "HospitalName";
            }
            else if (selectedCriteria == "Адреса")
            {
                searchColumn = "Addres";
            }
            else if (selectedCriteria == "Номер телефону")
            {
                searchColumn = "PhoneNumber";
            }
            else if (selectedCriteria == "HospitalID")
            {
                searchColumn = "HospitalID";
            }

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(searchColumn))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
                string searchQuery = $"SELECT * FROM Hospital WHERE {searchColumn} LIKE @SearchTerm";

                if (searchColumn == "HospitalID" && int.TryParse(searchText, out int hospitalId))
                {
                    searchQuery = $"SELECT * FROM Hospital WHERE {searchColumn} = @SearchTermInt";
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                            command.Parameters.AddWithValue("@SearchTermInt", hospitalId);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView2.DataSource = dataTable;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при пошуку за HospitalID: {ex.Message}");
                        }
                    }
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchText + "%");

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView2.DataSource = dataTable;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при пошуку в таблиці Hospital: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                AutoFillTable2();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Patient] (FullName, DateOfBirth, Addres, PhoneNumber, DoctorID) VALUES (@FullName, @DateOfBirth, @Addres, @PhoneNumber, @DoctorID)", sqlConnection);

            command.Parameters.AddWithValue("FullName", textBox19.Text);
            command.Parameters.AddWithValue("DateOfBirth", textBox20.Text);
            command.Parameters.AddWithValue("Addres", textBox21.Text);
            command.Parameters.AddWithValue("PhoneNumber", textBox22.Text);
            command.Parameters.AddWithValue("DoctorID", textBox23.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AutoFillTable3();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Patient SET FullName = @FullName, DateOfBirth = @DateOfBirth, Addres = @Addres, PhoneNumber = @PhoneNumber, DoctorID = @DoctorID WHERE PatientID = @PatientID;", sqlConnection);

            command.Parameters.AddWithValue("FullName", textBox24.Text);
            command.Parameters.AddWithValue("DateOfBirth", textBox25.Text);
            command.Parameters.AddWithValue("Addres", textBox26.Text);
            command.Parameters.AddWithValue("PhoneNumber", textBox27.Text);
            command.Parameters.AddWithValue("DoctorID", textBox28.Text);
            command.Parameters.AddWithValue("PatientID", textBox29.Text);

            MessageBox.Show(command.ExecuteNonQuery().ToString());
            AutoFillTable3();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox30.Text, out int patientIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deletePatientQuery = "DELETE FROM Patient WHERE PatientID = @PatientID;";
                        using (SqlCommand deleteCommand = new SqlCommand(deletePatientQuery, sqlConnection))
                        {
                            deleteCommand.Parameters.AddWithValue("@PatientID", patientIdToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int maxId = 0;
                                string getMaxIdQuery = "SELECT ISNULL(MAX(PatientID), 0) FROM Patient";
                                using (SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, sqlConnection))
                                {
                                    maxId = (int)getMaxIdCommand.ExecuteScalar();
                                }

                                string resetIdentityQuery = $"DBCC CHECKIDENT ('Patient', RESEED, {maxId})";
                                using (SqlCommand resetCommand = new SqlCommand(resetIdentityQuery, sqlConnection))
                                {
                                    resetCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Пацієнта успішно видалено.");
                                AutoFillTable3();
                                textBox30.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Пацієнта з таким ID не знайдено.");
                                AutoFillTable3();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Помилка при видаленні: " + ex.Message);
                        AutoFillTable3();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
                AutoFillTable3();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string searchText = textBox31.Text.Trim();
            string selectedCriteria = comboBox2.SelectedItem as string;
            string searchColumn = "";

            if (selectedCriteria == "Повне ім'я")
            {
                searchColumn = "FullName";
            }
            else if (selectedCriteria == "Дата народження")
            {
                searchColumn = "DateOfBirth";
            }
            else if (selectedCriteria == "Адреса")
            {
                searchColumn = "Addres";
            }
            else if (selectedCriteria == "Номер телефону")
            {
                searchColumn = "PhoneNumber";
            }
            else if (selectedCriteria == "DoctorID")
            {
                searchColumn = "DoctorID";
            }
            else if (selectedCriteria == "PatientID")
            {
                searchColumn = "PatientID";
            }

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(searchColumn))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
                string searchQuery = $"SELECT * FROM Patient WHERE {searchColumn} LIKE @SearchTerm";

                if (searchColumn == "DoctorID" || searchColumn == "PatientID")
                {
                    if (int.TryParse(searchText, out int idToSearch))
                    {
                        searchQuery = $"SELECT * FROM Patient WHERE {searchColumn} = @SearchTermInt";
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                sqlConnection.Open();
                                SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                                command.Parameters.AddWithValue("@SearchTermInt", idToSearch);

                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                if (dataGridView3 != null)
                                {
                                    dataGridView3.DataSource = dataTable;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Помилка при пошуку за {searchColumn}: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Будь ласка, введіть ціле число для пошуку за {searchColumn}.");
                    }
                }
                else if (searchColumn == "DateOfBirth")
                {
                    searchQuery = $"SELECT * FROM Patient WHERE {searchColumn} LIKE @SearchTerm";
                }
                else
                {
                    searchQuery = $"SELECT * FROM Patient WHERE {searchColumn} LIKE @SearchTerm";
                }

                if (searchColumn != "DoctorID" && searchColumn != "PatientID")
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchText + "%");

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataGridView3 != null)
                            {
                                dataGridView3.DataSource = dataTable;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при пошуку в таблиці Patient: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                AutoFillTable3();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Reception] (DateTime, PatientID, DoctorID, Diagnosis, Treatment, Amount) VALUES (@DateTime, @PatientID, @DoctorID, @Diagnosis, @Treatment, @Amount)", sqlConnection);

            DateTime dateTimeValue;
            if (DateTime.TryParse(textBox32.Text, out dateTimeValue))
            {
                command.Parameters.AddWithValue("DateTime", dateTimeValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть дату та час у коректному форматі.");
                return;
            }

            if (int.TryParse(textBox33.Text, out int patientIdValue))
            {
                command.Parameters.AddWithValue("PatientID", patientIdValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID пацієнта.");
                return;
            }

            if (int.TryParse(textBox34.Text, out int doctorIdValue))
            {
                command.Parameters.AddWithValue("DoctorID", doctorIdValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID лікаря.");
                return;
            }

            command.Parameters.AddWithValue("Diagnosis", textBox35.Text);

            command.Parameters.AddWithValue("Treatment", textBox36.Text);

            decimal amountValue;
            if (decimal.TryParse(textBox37.Text, out amountValue))
            {
                command.Parameters.AddWithValue("Amount", amountValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть числове значення для суми.");
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show($"Додано {rowsAffected} записів.");
                AutoFillTable4();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Помилка при додаванні прийому: {ex.Message}");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Reception SET DateTime = @DateTime, PatientID = @PatientID, DoctorID = @DoctorID, Diagnosis = @Diagnosis, Treatment = @Treatment, Amount = @Amount WHERE AppointmentID = @AppointmentID;", sqlConnection);

            DateTime dateTimeValue;
            if (DateTime.TryParse(textBox38.Text, out dateTimeValue))
            {
                command.Parameters.AddWithValue("DateTime", dateTimeValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть дату та час у коректному форматі.");
                return;
            }

            if (int.TryParse(textBox39.Text, out int patientIdValue))
            {
                command.Parameters.AddWithValue("PatientID", patientIdValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID пацієнта.");
                return;
            }

            if (int.TryParse(textBox40.Text, out int doctorIdValue))
            {
                command.Parameters.AddWithValue("DoctorID", doctorIdValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID лікаря.");
                return;
            }

            command.Parameters.AddWithValue("Diagnosis", textBox41.Text);

            command.Parameters.AddWithValue("Treatment", textBox42.Text);

            decimal amountValue;
            if (decimal.TryParse(textBox43.Text, out amountValue))
            {
                command.Parameters.AddWithValue("Amount", amountValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть числове значення для суми.");
                return;
            }

            if (int.TryParse(textBox44.Text, out int appointmentIdValue))
            {
                command.Parameters.AddWithValue("AppointmentID", appointmentIdValue);
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID прийому для оновлення.");
                return;
            }

            try
            {
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show($"Оновлено {rowsAffected} записів.");
                AutoFillTable4();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Помилка при оновленні прийому: {ex.Message}");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox45.Text, out int receptionIdToDelete))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        string deleteReceptionQuery = "DELETE FROM Reception WHERE AppointmentID = @AppointmentID;";
                        using (SqlCommand deleteCommand = new SqlCommand(deleteReceptionQuery, sqlConnection))
                        {
                            deleteCommand.Parameters.AddWithValue("@AppointmentID", receptionIdToDelete);
                            int rowsAffected = deleteCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                int maxId = 0;
                                string getMaxIdQuery = "SELECT ISNULL(MAX(AppointmentID), 0) FROM Reception";
                                using (SqlCommand getMaxIdCommand = new SqlCommand(getMaxIdQuery, sqlConnection))
                                {
                                    maxId = (int)getMaxIdCommand.ExecuteScalar();
                                }

                                string resetIdentityQuery = $"DBCC CHECKIDENT ('Reception', RESEED, {maxId})";
                                using (SqlCommand resetCommand = new SqlCommand(resetIdentityQuery, sqlConnection))
                                {
                                    resetCommand.ExecuteNonQuery();
                                }

                                MessageBox.Show("Запис про прийом успішно видалено.");
                                AutoFillTable4();
                                textBox45.Clear();
                            }
                            else
                            {
                                MessageBox.Show("Прийом з таким ID не знайдено.");
                                AutoFillTable4();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Помилка при видаленні: " + ex.Message);
                        AutoFillTable4();
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, введіть коректний числовий ID.");
                AutoFillTable4();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string searchText = textBox46.Text.Trim();
            string selectedCriteria = comboBox3.SelectedItem as string;
            string searchColumn = "";

            if (selectedCriteria == "DateTime")
            {
                searchColumn = "DateTime";
            }
            else if (selectedCriteria == "PatientID")
            {
                searchColumn = "PatientID";
            }
            else if (selectedCriteria == "DoctorID")
            {
                searchColumn = "DoctorID";
            }
            else if (selectedCriteria == "Diagnosis")
            {
                searchColumn = "Diagnosis";
            }
            else if (selectedCriteria == "Treatment")
            {
                searchColumn = "Treatment";
            }
            else if (selectedCriteria == "Amount")
            {
                searchColumn = "Amount";
            }
            else if (selectedCriteria == "AppointmentID")
            {
                searchColumn = "AppointmentID";
            }

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(searchColumn))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TestDB"].ConnectionString;
                string searchQuery = $"SELECT * FROM Reception WHERE {searchColumn} LIKE @SearchTerm";

                if (searchColumn == "PatientID" || searchColumn == "DoctorID" || searchColumn == "Amount" || searchColumn == "AppointmentID")
                {
                    if (int.TryParse(searchText, out int idToSearch))
                    {
                        searchQuery = $"SELECT * FROM Reception WHERE {searchColumn} = @SearchTermInt";
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            try
                            {
                                sqlConnection.Open();
                                SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                                command.Parameters.AddWithValue("@SearchTermInt", idToSearch);

                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                if (dataGridView4 != null) // Припускаємо, що dataGridView4 відображає таблицю Reception
                                {
                                    dataGridView4.DataSource = dataTable;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Помилка при пошуку за {searchColumn}: {ex.Message}");
                            }
                        }
                    }
                    else if (decimal.TryParse(searchText, out decimal amountToSearch) && searchColumn == "Amount")
                    {
                        searchQuery = $"SELECT * FROM Reception WHERE {searchColumn} = @SearchTermDecimal";
                        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                        {
                            // ... (аналогічний код для виконання запиту з @SearchTermDecimal)
                        }
                    }
                    else if (searchColumn == "PatientID" || searchColumn == "DoctorID" || searchColumn == "AppointmentID")
                    {
                        MessageBox.Show($"Будь ласка, введіть ціле число для пошуку за {searchColumn}.");
                    }
                }
                else if (searchColumn == "DateTime")
                {
                    searchQuery = $"SELECT * FROM Reception WHERE {searchColumn} LIKE @SearchTerm";
                    // ... (аналогічний код для виконання запиту за DateTime)
                }
                else
                {
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand command = new SqlCommand(searchQuery, sqlConnection);
                            command.Parameters.AddWithValue("@SearchTerm", "%" + searchText + "%");

                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataGridView4 != null)
                            {
                                dataGridView4.DataSource = dataTable;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Помилка при пошуку в таблиці Reception: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                AutoFillTable4(); // Припускаємо, що AutoFillTable4() заповнює DataGridView для таблиці Reception
            }
        }
    }
}
