using System;

/// <summary>
/// This is a brief description of this script
/// 
/// ##Script Description
/// Here is a more detailed description where we look more into what this entire script does
/// Be sure to indicate that this description is for the entire script and not just for a Function included in the
/// script.
/// 
/// ###A good way to do this?
/// 
/// Make use of Headers which involve the '#' sign.
/// Or you can emphasize different aspects via **BOLDING** and *ITALICIZING*
/// </summary>
public class ExampleScript
{
	private bool bVariable; /*!< @brief Random bool variable  */
	public int iVar;
	public double dEul;

	void Start()
    {
		

    }

	void Update()
    {
		
    }
	/**
	 * First line will be be a brief description of the function itself.
	 * 
	 * After the brief description add in a more detilaed description below. 
	 * 
	 *##Header1
	 * Some details you would like in this portion
	 * 
	 *###Header2
	 * Some other things
	 * 
	 *####Header3
	 *If you really want another
	 * 
	 * 
	 * @param a description of parameter 
	 * @param b description of parameter
	 * 
	 * @returns description of return type or the exact return of the function
	 * 
	 * @see RobotController()
	 * This will direct the user to the script listed with the at-see 
	 * @note b must be greater than 0 ( b > 0 )
	 * @attention please do not let b equal 0
	 * @warning if b equals 0 function will not work correctly
	 */
	public int Equation(int a , int b)
	{	///##Example
		///~~~~~~~~~~~~~~~~~~~~~~~~~.cpp
        ///int c = o; // This is where we are going to store the answer 
		///~~~~~~~~~~~~~~~~~~~~~~~~~
		int c = 0; 

		c = a / b;	// Keep inline comments in your scripts
		return 0;
	}


	/// <summary>
	/// This is again the brief description.
	/// 
	/// Here will again be the detailed description that could be used for the file.
	/// </summary>
	/// <param name="x">Using this style will generate the param for you</param> 
	/// <param name="y">Using this style will generate the param for you</param> 
	/// <returns>Multiplies x and y</returns> 
	/// @see RobotController() Can you still use this?
	/// @note I do hope so
	public int Equation2(int x, int y)
	{   
		int nu = 0;

		nu = x * y;  // These will still show up within Doxygen 
		return 0;
	}

	/*****************************************************************************************************
	 * I think I like this banner best.
	 * 
	 * It separates the comments and code better and makes it easier to tell where a commented block begins
	 * 
	 * @param w you still need to use these @ symbols inorder to create certain portions
	 * @param z but it is a necceesary evil
	 * 
	 * ##Another Header
	 * Because i need to be certain
	 * 
	 * ###Another Header2??
	 * 
	 * can I **Bold** things as well? How about *italicize*?
	 * 
	 ***********************************************************/
	public int Equation3(int w, int z)
	{   
		int gamma = 0;

		gamma = 2*w + z/2;  // But only when you go to look at the direct code
		return 0;
	}

	/******************************************************************************
	 * Simple Math equation using two parameters
	 * 
	 * This Function will take parameter ab and bc, manipulate them to find the answer to a math equation
	 * 
	 * @param ab the divider for the poistive number
	 * @param bc the divider for the number being subtracted
	 * 
	 * @note Be sure ot look at the definition within the script to see the full scope of what is going on
	 * 
	 * @warning both ab and bc must be greater than 0 ( ab > 0 && bc > 0 )
	 *************************************************/
	public int Equation4(int ab, int bc)
	{
		int alpha = 0;  

		alpha = 5+bc - ab/10;  // Instead of showing up in the HTML file itself like the XML comments and the like
		return 0;
	}
}
