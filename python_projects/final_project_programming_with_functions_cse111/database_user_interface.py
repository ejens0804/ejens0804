# This program is a database query tool that allows users to connect to a MySQL database, execute SQL queries, and perform additional actions such as listing databases 
# and tables, showing table structure, and exporting query results to a CSV file. The program uses the mysql-connector-python library to connect to a MySQL database 
# and tkinter for the graphical user interface.

# At this point, I haven't yet taken a class that teaches programming with classes, so I had to teach myself some of the concepts necessary to understand this code.  
# I utilized resources on the internet such as tutorials, articles discussing specific topics, and AI models.  These resources helped me understand the code and how to
# write the test cases for the code.  I also used the Python documentation to understand the unittest.mock module and how to use it in the test cases.

# This was a really good learning experience, and I now better understand how to write code that is maintainable and testable. I also learned how to use the unittest.mock
# module to mock objects and functions in Python.  This will be very useful in the future when I need to write test cases for my code.


# This program is broken down into the following functions:
# 1. connect_to_database: Connects to a MySQL database using the connection details provided by the user.
# 2. disconnect_database: Properly closes the database connection.
# 3. save_connection_profile: Saves the current connection details to a JSON configuration file.
# 4. load_connection_profile: Loads a previously saved connection profile.
# 5. execute_query: Executes the SQL query entered by the user and displays the results in a treeview widget.
# 6. save_query: Saves the current query to a file.
# 7. load_query: Loads a previously saved query from a file.
# 8. export_results: Exports the query results to a CSV file.
# 9. show_table_structure: Displays the structure of a selected table.
# 10. list_databases: Lists all available databases on the connected server.
# 11. list_tables: Lists all tables in the currently selected database.

# These functions are housed within a class called "DatabaseQueryApp".  The class has an __init__ method that initializes the GUI elements and sets up the layout of the
# application.  The class also contains instance variables to store the database connection and other GUI elements.  The class methods interact with the GUI elements and
# the database connection to perform the required actions.


import tkinter as tk
from tkinter import messagebox, simpledialog, ttk
import mysql.connector
import json
import csv

class DatabaseQueryApp:
    def __init__(self, master):
        self.master = master
        master.title("MySQL Database Query Tool")
        master.geometry("1000x800")  # Increased window size to accommodate features

        # Connection Details Frame
        self.connection_frame = tk.LabelFrame(master, text="Database Connection")
        self.connection_frame.pack(padx=10, pady=10, fill="x")

        # Connection Inputs
        tk.Label(self.connection_frame, text="Host:").grid(row=0, column=0, padx=5, pady=5)
        self.host_entry = tk.Entry(self.connection_frame, width=20)
        self.host_entry.grid(row=0, column=1, padx=5, pady=5)
        self.host_entry.insert(0, "localhost")

        tk.Label(self.connection_frame, text="User:").grid(row=0, column=2, padx=5, pady=5)
        self.user_entry = tk.Entry(self.connection_frame, width=20)
        self.user_entry.grid(row=0, column=3, padx=5, pady=5)

        tk.Label(self.connection_frame, text="Password:").grid(row=1, column=0, padx=5, pady=5)
        self.password_entry = tk.Entry(self.connection_frame, show="*", width=20)
        self.password_entry.grid(row=1, column=1, padx=5, pady=5)

        tk.Label(self.connection_frame, text="Database:").grid(row=1, column=2, padx=5, pady=5)
        self.database_entry = tk.Entry(self.connection_frame, width=20)
        self.database_entry.grid(row=1, column=3, padx=5, pady=5)

        # Connection Buttons Frame
        self.connection_buttons_frame = tk.Frame(self.connection_frame)
        self.connection_buttons_frame.grid(row=2, column=0, columnspan=4, pady=10)

        # Connect Button
        self.connect_button = tk.Button(self.connection_buttons_frame, text="Connect", command=self.connect_to_database)
        self.connect_button.pack(side=tk.LEFT, padx=5)

        # Save Profile Button
        self.save_profile_button = tk.Button(self.connection_buttons_frame, text="Save Profile", command=self.save_connection_profile)
        self.save_profile_button.pack(side=tk.LEFT, padx=5)

        # Load Profile Button
        self.load_profile_button = tk.Button(self.connection_buttons_frame, text="Load Profile", command=self.load_connection_profile)
        self.load_profile_button.pack(side=tk.LEFT, padx=5)

        # Disconnect Button
        self.disconnect_button = tk.Button(self.connection_buttons_frame, text="Disconnect", command=self.disconnect_database, state=tk.DISABLED)
        self.disconnect_button.pack(side=tk.LEFT, padx=5)

        # Query Frame
        self.query_frame = tk.LabelFrame(master, text="SQL Query")
        self.query_frame.pack(padx=10, pady=10, fill="both", expand=True)

        # Query Input
        self.query_text = tk.Text(self.query_frame, height=10, width=80)
        self.query_text.pack(padx=10, pady=10, fill="x")

        # Query Buttons Frame
        self.query_buttons_frame = tk.Frame(self.query_frame)
        self.query_buttons_frame.pack(pady=10)

        # Execute Button
        self.execute_button = tk.Button(self.query_buttons_frame, text="Execute Query", command=self.execute_query, state=tk.DISABLED)
        self.execute_button.pack(side=tk.LEFT, padx=5)

        # Save Query Button
        self.save_query_button = tk.Button(self.query_buttons_frame, text="Save Query", command=self.save_query, state=tk.DISABLED)
        self.save_query_button.pack(side=tk.LEFT, padx=5)

        # Load Query Button
        self.load_query_button = tk.Button(self.query_buttons_frame, text="Load Query", command=self.load_query)
        self.load_query_button.pack(side=tk.LEFT, padx=5)

        # Additional Database Actions Frame
        self.actions_frame = tk.LabelFrame(master, text="Database Actions")
        self.actions_frame.pack(padx=10, pady=10, fill="x")

        # Additional Actions Buttons
        self.actions_buttons_frame = tk.Frame(self.actions_frame)
        self.actions_buttons_frame.pack(pady=10)

        # Show Table Structure Button
        self.table_structure_button = tk.Button(self.actions_buttons_frame, text="Show Table Structure", 
                                                command=self.show_table_structure, state=tk.DISABLED)
        self.table_structure_button.pack(side=tk.LEFT, padx=5)

        # List Databases Button
        self.list_databases_button = tk.Button(self.actions_buttons_frame, text="List Databases", command=self.list_databases, state=tk.DISABLED)
        self.list_databases_button.pack(side=tk.LEFT, padx=5)

        # List Tables Button
        self.list_tables_button = tk.Button(self.actions_buttons_frame, text="List Tables", command=self.list_tables, state=tk.DISABLED)
        self.list_tables_button.pack(side=tk.LEFT, padx=5)
        
        # Export Results Button
        self.export_results_button = tk.Button(self.actions_buttons_frame, text="Export Results", command=self.export_results, state=tk.DISABLED)
        self.export_results_button.pack(side=tk.LEFT, padx=5)

        # Results Frame
        self.results_frame = tk.LabelFrame(master, text="Query Results")
        self.results_frame.pack(padx=10, pady=10, fill="both", expand=True)

        # Results Treeview
        self.results_tree = ttk.Treeview(self.results_frame)
        self.results_tree.pack(padx=10, pady=10, fill="both", expand=True)

        # Scrollbar for Results
        self.scrollbar = ttk.Scrollbar(self.results_frame, orient="vertical", command=self.results_tree.yview)
        self.scrollbar.pack(side="right", fill="y")
        self.results_tree.configure(yscrollcommand=self.scrollbar.set)

        # Instance variables
        self.connection = None

    def connect_to_database(self):
        try:
            self.connection = mysql.connector.connect(
                host=self.host_entry.get(),
                user=self.user_entry.get(),
                password=self.password_entry.get(),
                database=self.database_entry.get()
            )
            messagebox.showinfo("Success", "Database Connected Successfully!")
            
            # Enable buttons
            self.execute_button.config(state=tk.NORMAL)
            self.save_query_button.config(state=tk.NORMAL)
            self.disconnect_button.config(state=tk.NORMAL)
            self.table_structure_button.config(state=tk.NORMAL)
            self.list_databases_button.config(state=tk.NORMAL)
            self.export_results_button.config(state=tk.DISABLED)  # Only enable after a query
            self.list_tables_button.config(state=tk.NORMAL)
        except mysql.connector.Error as err:
            messagebox.showerror("Connection Error", str(err))

    def disconnect_database(self):
        """
        Properly close the database connection.
        """
        if self.connection:
            self.connection.close()
            self.connection = None
            
            # Disable buttons
            self.execute_button.config(state=tk.DISABLED)
            self.save_query_button.config(state=tk.DISABLED)
            self.disconnect_button.config(state=tk.DISABLED)
            self.table_structure_button.config(state=tk.DISABLED)
            self.list_databases_button.config(state=tk.DISABLED)
            self.export_results_button.config(state=tk.DISABLED)
            self.list_tables_button.config(state=tk.DISABLED)
            
            messagebox.showinfo("Disconnected", "Database connection closed.")

    def save_connection_profile(self):
        """
        Save current connection details to a JSON configuration file.
        """
        connection_profile = {
            'host': self.host_entry.get(),
            'user': self.user_entry.get(),
            'database': self.database_entry.get()
        }
        
        filename = simpledialog.askstring("Save Profile", "Enter profile name:")
        if filename:
            try:
                with open(f"{filename}_profile.json", 'w') as f:
                    json.dump(connection_profile, f)
                messagebox.showinfo("Saved", f"Connection profile saved as {filename}_profile.json")
            except Exception as e:
                messagebox.showerror("Error", f"Could not save profile: {str(e)}")

    def load_connection_profile(self):
        """
        Load a previously saved connection profile.
        """
        filename = simpledialog.askstring("Load Profile", "Enter profile name:")
        if filename:
            try:
                with open(f"{filename}_profile.json", 'r') as f:
                    profile = json.load(f)
                
                self.host_entry.delete(0, tk.END)
                self.host_entry.insert(0, profile.get('host', 'localhost'))
                
                self.user_entry.delete(0, tk.END)
                self.user_entry.insert(0, profile.get('user', ''))
                
                self.database_entry.delete(0, tk.END)
                self.database_entry.insert(0, profile.get('database', ''))
                
                messagebox.showinfo("Loaded", f"Connection profile {filename}_profile.json loaded successfully!")
            except FileNotFoundError:
                messagebox.showerror("Error", f"Profile file {filename}_profile.json not found.")
            except Exception as e:
                messagebox.showerror("Error", f"Could not load profile: {str(e)}")

    def execute_query(self):
        # Clear previous results
        for i in self.results_tree.get_children():
            self.results_tree.delete(i)

        try:
            # Create cursor
            cursor = self.connection.cursor(dictionary=True)
            query = self.query_text.get("1.0", tk.END).strip()

            # Execute query
            cursor.execute(query)

            # For SELECT queries, display results
            if query.lower().startswith('select'):
                # Get column names
                columns = [col[0] for col in cursor.description]
                
                # Configure treeview columns
                self.results_tree["columns"] = columns
                self.results_tree["show"] = "headings"
                
                # Create column headings
                for col in columns:
                    self.results_tree.heading(col, text=col)
                    self.results_tree.column(col, width=100)

                # Insert data
                for row in cursor.fetchall():
                    self.results_tree.insert("", "end", values=[row[col] for col in columns])
                
                messagebox.showinfo("Query Result", f"Retrieved {len(self.results_tree.get_children())} rows")
                
                # Enable export results button
                self.export_results_button.config(state=tk.NORMAL)
            
            # For INSERT, UPDATE, DELETE queries
            else:
                self.connection.commit()
                messagebox.showinfo("Query Result", "Query executed successfully!")

            cursor.close()

        except mysql.connector.Error as err:
            messagebox.showerror("Query Error", str(err))

    def save_query(self):
        """
        Save the current query to a file.
        """
        query = self.query_text.get("1.0", tk.END).strip()
        filename = simpledialog.askstring("Save Query", "Enter filename:")
        
        if filename:
            try:
                with open(f"{filename}.sql", "w") as f:
                    f.write(query)
                messagebox.showinfo("Saved", f"Query saved as {filename}.sql")
            except Exception as e:
                messagebox.showerror("Error", f"Could not save query: {str(e)}")

    def load_query(self):
        """
        Load a previously saved query from a file.
        """
        filename = simpledialog.askstring("Load Query", "Enter filename:")
        
        if filename:
            try:
                with open(f"{filename}.sql", "r") as f:
                    query = f.read()
                
                self.query_text.delete("1.0", tk.END)
                self.query_text.insert("1.0", query)
                messagebox.showinfo("Loaded", f"Query loaded from {filename}.sql")
            except FileNotFoundError:
                messagebox.showerror("Error", f"File {filename}.sql not found.")
            except Exception as e:
                messagebox.showerror("Error", f"Could not load query: {str(e)}")

    def export_results(self):
        """
        Export query results to a CSV file.
        """
        if not self.results_tree.get_children():
            messagebox.showwarning("Export", "No results to export.")
            return
        
        filename = simpledialog.askstring("Export", "Enter filename for CSV:")
        
        if filename:
            try:
                with open(f"{filename}.csv", "w", newline='') as csvfile:
                    csv_writer = csv.writer(csvfile)
                    
                    # Write headers
                    headers = self.results_tree["columns"]
                    csv_writer.writerow(headers)
                    
                    # Write data
                    for item in self.results_tree.get_children():
                        csv_writer.writerow(self.results_tree.item(item)['values'])
                
                messagebox.showinfo("Exported", f"Results exported to {filename}.csv")
            except Exception as e:
                messagebox.showerror("Error", f"Could not export results: {str(e)}")

    def show_table_structure(self):
        """
        Display the structure of a selected table.
        """
        table_name = simpledialog.askstring("Table Structure", "Enter table name:")
        
        if table_name and self.connection:
            try:
                cursor = self.connection.cursor(dictionary=True)
                
                # Clear previous results
                for i in self.results_tree.get_children():
                    self.results_tree.delete(i)
                
                # Get table structure
                cursor.execute(f"DESCRIBE {table_name}")
                
                # Configure treeview columns
                columns = ['Field', 'Type', 'Null', 'Key', 'Default', 'Extra']
                self.results_tree["columns"] = columns
                self.results_tree["show"] = "headings"
                
                for col in columns:
                    self.results_tree.heading(col, text=col)
                    self.results_tree.column(col, width=100)
                
                # Insert data
                for row in cursor.fetchall():
                    self.results_tree.insert("", "end", values=[
                        row['Field'], row['Type'], row['Null'], 
                        row['Key'], row['Default'], row['Extra']
                    ])
                
                cursor.close()
                messagebox.showinfo("Table Structure", f"Structure of {table_name} retrieved.")
                
                # Enable export results button
                self.export_results_button.config(state=tk.NORMAL)
            
            except mysql.connector.Error as err:
                messagebox.showerror("Error", str(err))

    def list_databases(self):
        """
        List all available databases on the connected server.
        """
        if self.connection:
            try:
                cursor = self.connection.cursor()
                
                # Clear previous results
                for i in self.results_tree.get_children():
                    self.results_tree.delete(i)
                
                cursor.execute("SHOW DATABASES")
                
                # Configure treeview columns
                self.results_tree["columns"] = ["Database"]
                self.results_tree["show"] = "headings"
                self.results_tree.heading("Database", text="Database")
                self.results_tree.column("Database", width=200)
                
                # Insert database names
                for (database,) in cursor.fetchall():
                    self.results_tree.insert("", "end", values=[database])
                
                cursor.close()
                messagebox.showinfo("Databases", "List of databases retrieved.")
                
                # Enable export results button
                self.export_results_button.config(state=tk.NORMAL)
            
            except mysql.connector.Error as err:
                messagebox.showerror("Error", str(err))

    def list_tables(self):
        """
        List all tables in the currently selected database.
        """
        if self.connection:
            try:
                cursor = self.connection.cursor()
                
                # Clear previous results
                for i in self.results_tree.get_children():
                    self.results_tree.delete(i)
                
                # Get current database name
                cursor.execute("SELECT DATABASE()")
                current_db = cursor.fetchone()[0]
                
                # Query to list all tables in the current database
                cursor.execute("SHOW TABLES")
                
                # Configure treeview columns
                self.results_tree["columns"] = ["Table Name"]
                self.results_tree["show"] = "headings"
                self.results_tree.heading("Table Name", text="Tables in Database: " + str(current_db))
                self.results_tree.column("Table Name", width=300)
                
                # Insert table names
                for (table,) in cursor.fetchall():
                    self.results_tree.insert("", "end", values=[table])
                
                cursor.close()
                messagebox.showinfo("Tables", f"Retrieved tables from database: {current_db}")
                
                # Enable export results button
                self.export_results_button.config(state=tk.NORMAL)
            
            except mysql.connector.Error as err:
                messagebox.showerror("Error", str(err))
                
def main():
    root = tk.Tk()
    app = DatabaseQueryApp(root)
    root.mainloop()

if __name__ == "__main__":
    main()
