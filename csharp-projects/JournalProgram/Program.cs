using System;
using System.IO;


// I fulfilled the Core Requirements of this program!
// The additional things that I added to go above and beyond were
// writing error handling for empty user inputs when accepting input
// in the menu, and I created an extra menu option to list the
// journal files that have been previously saved so the user can continue
// to edit them or read them without having to go to their directory to 
// read the file names.




class Program
{
    static void Main(string[] args)
    {
        string menuOptionUserSelected = "";
        Menu menu1 = new Menu();

        while (menuOptionUserSelected != "6")
        {
            menu1.DisplayMenu();
            menuOptionUserSelected = menu1.GetUserMenuChoice();

            while (menuOptionUserSelected != "1" && menuOptionUserSelected != "2" && menuOptionUserSelected != "3" && menuOptionUserSelected != "4" && menuOptionUserSelected != "5" && menuOptionUserSelected != "6")
            {
                Console.WriteLine("Input Invalid. Please try again");
                menuOptionUserSelected = menu1.GetUserMenuChoice();
            }
            menu1.RunMenuChoice(menuOptionUserSelected);

        }

    }

}