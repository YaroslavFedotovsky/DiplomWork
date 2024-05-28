using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;


namespace AutDataProcSystem
{
    public class DatabaseConnect
    {
        private NpgsqlConnection connection;

        //Метод подключения к базе данных
        public DatabaseConnect()
        {
            string connectionString = "Host=127.0.0.1;Port=5432;Database=DataSystem;Username=postgres;Password=12345678";
            connection = new NpgsqlConnection(connectionString);
        }

        //Метод открытия соединения с БД
        public void OpenConnection()
        {
            try
            {
                connection.Open();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка подключения к базе данных: " + ex.Message);
            }
        }

        //Метод закрытия соединения с БД
        public void CloseConnection()
        {
            connection.Close();
        }

        public DataTable SelectData(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка запроса " + ex.Message);
            }

            return dataTable;
        }

        //Метод для авторизации пользователя, используется для получения данных об организации пользователя
        public bool AuthenticateUser(string login, string password)
        {
            bool isAuthenticated = false;

            try
            {
                OpenConnection();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE login = @login AND password = @password", connection))
                {
                    cmd.Parameters.AddWithValue("login", login);
                    cmd.Parameters.AddWithValue("password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    isAuthenticated = count > 0;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return isAuthenticated;
        }

        //Получение данных о культуре
        public DataTable GetCultures()
        {
            string query = "SELECT id, culture_name FROM Cultures";
            return SelectData(query);
        }

        //Получение данных об организации
        public DataTable GetOrganization()
        {
            string query = "SELECT id, name FROM Organization";
            return SelectData(query);
        }

        //Получение данных о ролях
        public DataTable GetRole()
        {
            string query = "SELECT id, role FROM Roles";
            return SelectData(query);
        }

        //Получение данных об организации пользователя
        public string GetOrganizationNameForUser(string login, string password)
        {
            string organizationName = "";

            try
            {
                OpenConnection();

                string query = "SELECT organization.name " +
                               "FROM users " +
                               "JOIN organization ON users.organization_id = organization.id " +
                               "WHERE users.login = @login AND users.password = @password";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    DataTable resultTable = SelectDataOrganization(cmd);

                    if (resultTable.Rows.Count > 0)
                    {
                        organizationName = resultTable.Rows[0]["name"].ToString();
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return organizationName;
        }

        //Вспомогательный класс для метода GetOrganizationNameForUser
        public DataTable SelectDataOrganization(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса: " + ex.Message);
            }

            return dataTable;
        }

        //Проверка роли пользователя
        public int GetUserRoleId(string login, string password)
        {
            int userRoleId = -1; // Значение по умолчанию или код ошибки, если роль не найдена.

            try
            {
                OpenConnection();

                string query = "SELECT roles.id " +
                               "FROM users " +
                               "JOIN roles ON users.role_id = roles.id " +
                               "WHERE users.login = @login AND users.password = @password";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    DataTable resultTable = SelectDataOrganization(cmd);

                    if (resultTable.Rows.Count > 0)
                    {
                        // Преобразуйте значение в int.
                        userRoleId = Convert.ToInt32(resultTable.Rows[0]["id"]);
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return userRoleId;
        }

        //Метод для DataGrid по объему выполненных работ (в кабинете начальника)
        public void DisplayFormsForOrganization(string selectedOrganization, System.Windows.Controls.DataGrid dataGrid)
        {
            try
            {
                OpenConnection();

                // Получаем ID организации по её имени
                string orgIdQuery = "SELECT id FROM organization WHERE name = @organizationName";

                using (NpgsqlCommand orgIdCmd = new NpgsqlCommand(orgIdQuery, connection))
                {
                    orgIdCmd.Parameters.AddWithValue("@organizationName", selectedOrganization);
                    int organizationId = Convert.ToInt32(orgIdCmd.ExecuteScalar());

                    // Получаем формы для заполнения для данной организации, включая сведения о выращиваемой культуре и стадии
                    string formsQuery = "SELECT stage.name, cultures.culture_name, forms_to_fill_out.values, to_char(forms_to_fill_out.date_filling, 'yyyy-mm-dd') " +
                                        "FROM forms_to_fill_out " +
                                        "JOIN cultures ON forms_to_fill_out.culture_id = cultures.id " +
                                        "JOIN stage ON forms_to_fill_out.stage_id = stage.id " +
                                        "WHERE forms_to_fill_out.org_id = @organizationId";

                    using (NpgsqlCommand formsCmd = new NpgsqlCommand(formsQuery, connection))
                    {
                        formsCmd.Parameters.AddWithValue("@organizationId", organizationId);

                        DataTable formsTable = new DataTable();
                        formsTable.Load(formsCmd.ExecuteReader());

                        // Установка источника данных для DataGrid
                        dataGrid.ItemsSource = formsTable.DefaultView;
                    }
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //Метод для выпадающего списка организаций (в кабинете Начальника)
        public void FillOrganizationComboBox(System.Windows.Controls.ComboBox comboBox)
        {
            try
            {
                OpenConnection();

                string query = "SELECT DISTINCT name FROM organization WHERE NOT id=4";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    NpgsqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string organizationName = reader["name"].ToString();
                        comboBox.Items.Add(organizationName);
                    }

                    reader.Close();
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        //Метод возвращает ID названия культуры
        public int GetCultureIdByName(string cultureName)
        {
            string query = $"SELECT id FROM cultures WHERE culture_name = '{cultureName}'";
            DataTable result = SelectData(query);
            return result.Rows.Count > 0 ? (int)result.Rows[0]["Id"] : -1; // Возвращает id наименования культуры. В противном случае ставит -1
        }

        //Метод возвращает ID названия организации
        public int GetOrgIdByName(string orgName)
        {
            string query = $"SELECT id FROM Organization WHERE name = '{orgName}'";
            DataTable result = SelectData(query);
            return result.Rows.Count > 0 ? (int)result.Rows[0]["Id"] : -1; // Возвращает id наименования культуры. В противном случае ставит -1
        }

        //Метод возвращает ID названия роли
        public int GetRoleIdByName(string roleName)
        {
            string query = $"SELECT id FROM Roles WHERE role = '{roleName}'";
            DataTable result = SelectData(query);
            return result.Rows.Count > 0 ? (int)result.Rows[0]["Id"] : -1; // Возвращает id наименования культуры. В противном случае ставит -1
        }

        //Метод сохранения данных с формы в БД
        public void SaveForms(int orgId, int stageId, int cultureId, int volumeWork)
        {
            try
            {
                OpenConnection();

                // Проверка на отсутствие формы с такой же стадией обработки, такой же культурой и у такой же организации
                string checkExistenceQuery = $"SELECT COUNT(*) FROM forms_to_fill_out WHERE stage_id = '{stageId}' AND culture_id = {cultureId} AND org_id = {orgId}";
                int count = Convert.ToInt32(SelectData(checkExistenceQuery).Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("Форма с такими данными уже существует.");
                    return;
                }

                // Сохранение формы данных в БД
                string addModuleQuery = $"INSERT INTO Forms_to_fill_out (org_id, culture_id, stage_id, values, date_filling) VALUES ({orgId}, {cultureId}, {stageId}, {volumeWork}, current_timestamp)";
                ExecuteNonQuery(addModuleQuery);

                MessageBox.Show("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveNewUser(string log, string pass, string nameUs, string phoneUs, string emailUs, int orgId, int roleId)
        {
            try
            {
                OpenConnection();


                // Проверка на отсутствие формы с такой же стадией обработки, такой же культурой и у такой же организации
                string checkExistenceQuery = $"SELECT COUNT(*) FROM users WHERE login = '{log}'";
                int count = Convert.ToInt32(SelectData(checkExistenceQuery).Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.");
                    return;
                }

                // Сохранение формы данных в БД
                string addModuleQuery = $"INSERT INTO users (login, password, name, phone, email, organization_id, role_id) VALUES ('{log}', '{pass}', '{nameUs}', '{phoneUs}', '{emailUs}', {orgId}, {roleId})";
                ExecuteNonQuery(addModuleQuery);

                MessageBox.Show("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveNewOrg(string orgName)
        {
            try
            {
                OpenConnection();

                // Проверка на отсутствие такой же организации
                string checkExistenceQuery = $"SELECT COUNT(*) FROM organization WHERE name = '{orgName}'";
                int count = Convert.ToInt32(SelectData(checkExistenceQuery).Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("Организация с таким названием уже существует.");
                    return;
                }

                // Сохранение формы данных в БД
                string addModuleQuery = $"INSERT INTO organization (name) VALUES ('{orgName}')";
                ExecuteNonQuery(addModuleQuery);

                MessageBox.Show("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void SaveNewCult(string cultName)
        {
            try
            {
                OpenConnection();

                // Проверка на отсутствие такой же организации
                string checkExistenceQuery = $"SELECT COUNT(*) FROM cultures WHERE culture_name = '{cultName}'";
                int count = Convert.ToInt32(SelectData(checkExistenceQuery).Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("Культура с таким названием уже существует.");
                    return;
                }

                // Сохранение формы данных в БД
                string addModuleQuery = $"INSERT INTO cultures (culture_name) VALUES ('{cultName}')";
                ExecuteNonQuery(addModuleQuery);

                MessageBox.Show("Данные успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при добавлении данных: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void ExecuteNonQuery(string query)
        {
            try
            {
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения SQL-запроса: " + ex.Message);
            }
        }


    }

        
}
