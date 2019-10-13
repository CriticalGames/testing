public string[,] SQLiteTableToArray(string tablename, SQLiteConnection sqlcon)
        {
            int cols = 0, rows = 0;
            /*get number of columns in table*/
            /*only need to read one row*/
            string sqlstr = "SELECT * FROM " + tablename + " LIMIT 1";
            SQLiteCommand cmd = new SQLiteCommand(sqlstr, sqlcon);
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();
            /*loop and increment until exception is thrown*/
            while (true)
            {
                try
                {
                    reader.GetValue(cols);
                    cols++;
                }
                catch (Exception)
                {
                    break;
                }
            }
            
            /*get number of columns in table*/
			/*no need to read the whole table and loop*/
            sqlstr = "SELECT COUNT(*) FROM " + tablename;
            cmd = new SQLiteCommand(sqlstr, sqlcon);
            object v = cmd.ExecuteScalar();
            rows = Convert.ToInt32(v);
            v = null;
            /*create a new array with correct row and column count*/
            string[,] array = new string[rows, cols];
            /*read table into array*/
            sqlstr = "SELECT * FROM " + tablename;
            cmd = new SQLiteCommand(sqlstr, sqlcon);
            reader = cmd.ExecuteReader();
             for (int y = 0; y < rows; y++)
            {
                reader.Read();
                for (int x = 0; x < cols; x++)
                {
                    array[y, x] = reader.GetValue(x).ToString();
                }
            }
            /*clean up*/
            cmd.Dispose();
            reader.Close();
            return array;
        }
