using System;

public class Program
{
	public static void Main()
	{
		Console.WriteLine("Code For Robot Wars Challenge");
		
		// Test Input is taken in string	
		string testInput = "5 5\n1 2 N\nLMLMLMLMM\n3 3 E\nMMRMMRMRRMM";
		Console.WriteLine("\n***Input***");
		Console.WriteLine(testInput);

		// Splitting test input by new line and storing it in array
		string[] inputArray = testInput.Split('\n');
		
		int upperX = Convert.ToInt32(inputArray[0].Split(' ')[0]);
		int upperY = Convert.ToInt32(inputArray[0].Split(' ')[1]);
				
		Console.WriteLine("\n***Output***");

		// Logic to find the position of robots
		for(int i = 1; i < inputArray.Length; i=i+2) {
			
			// Robot's x,y coordinate and heading is stored in variables
			string position = inputArray[i];
			int currentPositionX = Convert.ToInt32(position.Split(' ')[0]);
			int currentPositionY = Convert.ToInt32(position.Split(' ')[1]);
			string currentHeading = position.Split(' ')[2];


			// Robot's position is stored in a variable
			string instructions = inputArray[i+1];

			// Looping through instruction to get the current position 
			// and current heading of the robot
			foreach(char c in instructions) {
				if(c.Equals('L')) {
					if (currentHeading.Equals("N")) {
						currentHeading = "W";
					}
					else if(currentHeading.Equals("W")) {
						currentHeading = "S";
					}
					else if(currentHeading.Equals("S")) {
						currentHeading = "E";
					}
					else if(currentHeading.Equals("E")) {
						currentHeading = "N";
					}
				} else if(c.Equals('R')) {
					if (currentHeading.Equals("N")) {
						currentHeading = "E";
					}
					else if(currentHeading.Equals("E")) {
						currentHeading = "S";
					}
					else if(currentHeading.Equals("S")) {
						currentHeading = "W"; 
					}
					else if(currentHeading.Equals("W")) {
						currentHeading = "N";
					}
				} else if(c.Equals('M')) {
					
						if(currentHeading.Equals("N")) {
							if (currentPositionY+1 <= upperY) currentPositionY = currentPositionY + 1;
							else {Console.WriteLine("Robot out of arena, please give valid instrictions for this robot");return;}
						}
						else if(currentHeading.Equals("E")) { 
							if(currentPositionX+1 <= upperX) currentPositionX = currentPositionX + 1;
							else {Console.WriteLine("Robot out of arena, please give valid instrictions");return;}
						}
						else if(currentHeading.Equals("S")) {
							if(currentPositionY-1 >=0) currentPositionY = currentPositionY - 1;
							else {Console.WriteLine("Robot out of arena, please give valid instrictions");return;}
						}
						else if(currentHeading.Equals("W")) {
							if(currentPositionX-1 >= 0) currentPositionX = currentPositionX - 1;
							else {Console.WriteLine("Robot out of arena, please give valid instrictions");return;}
						}	
					}
				}
			
			// Displaying output (current position of the robot)
			Console.WriteLine(currentPositionX + " " + currentPositionY + " " + currentHeading);
		}
	}	
}

