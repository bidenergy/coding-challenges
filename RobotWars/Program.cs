using System;
using System.Collections.Generic;

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Code For Robot Wars Challenge");
		
		// Test Input is taken in string	
		string testInput = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRM";
		
		// Splitting test input by new line and storing it in array
		string[] inputArray = testInput.Split('\n');
		
		int upperX = Convert.ToInt32(inputArray[0].Split(' ')[0]);
		int upperY = Convert.ToInt32(inputArray[0].Split(' ')[1]);
		
		List<String> output = new List<String>();
		
		
	}
}