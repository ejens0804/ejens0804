import unittest
from unittest.mock import MagicMock, patch
import tkinter as tk
from database_user_interface import DatabaseQueryApp

class TestDatabaseQueryApp(unittest.TestCase):
    def setUp(self):
        self.root = tk.Tk()
        self.app = DatabaseQueryApp(self.root)

    def tearDown(self):
        self.root.destroy()

    @patch('mysql.connector.connect')
    def test_connect_to_database(self, mock_connect):
        mock_connect.return_value = MagicMock()
        self.app.host_entry.insert(0, 'localhost')
        self.app.user_entry.insert(0, 'root')
        self.app.password_entry.insert(0, 'password')
        self.app.database_entry.insert(0, 'test_db')
        self.app.connect_to_database()
        self.assertIsNotNone(self.app.connection)

    def test_disconnect_database(self):
        self.app.connection = MagicMock()
        self.app.disconnect_database()
        self.assertIsNone(self.app.connection)

    @patch("builtins.open", new_callable=unittest.mock.mock_open)
    @patch("json.dump")
    def test_save_connection_profile(self, mock_json_dump, mock_open):
        with patch("tkinter.simpledialog.askstring", return_value="test_profile"):
            self.app.save_connection_profile()
        mock_open.assert_called_with("test_profile_profile.json", "w")
        mock_json_dump.assert_called()

    @patch("builtins.open", new_callable=unittest.mock.mock_open, read_data='{"host": "localhost", "user": "root", "database": "test_db"}')
    @patch("json.load", return_value={"host": "localhost", "user": "root", "database": "test_db"})
    def test_load_connection_profile(self, mock_json_load, mock_open):
        with patch("tkinter.simpledialog.askstring", return_value="test_profile"):
            self.app.load_connection_profile()
        self.assertEqual(self.app.host_entry.get(), "localhost")
        self.assertEqual(self.app.user_entry.get(), "root")
        self.assertEqual(self.app.database_entry.get(), "test_db")

    @patch("builtins.open", new_callable=unittest.mock.mock_open)
    def test_save_query(self, mock_open):
        self.app.query_text.insert("1.0", "SELECT * FROM users;")
        with patch("tkinter.simpledialog.askstring", return_value="test_query"):
            self.app.save_query()
        mock_open.assert_called_with("test_query.sql", "w")

    @patch("builtins.open", new_callable=unittest.mock.mock_open, read_data="SELECT * FROM users;")
    def test_load_query(self, mock_open):
        with patch("tkinter.simpledialog.askstring", return_value="test_query"):
            self.app.load_query()
        self.assertEqual(self.app.query_text.get("1.0", tk.END).strip(), "SELECT * FROM users;")

if __name__ == "__main__":
    unittest.main()
