#include <stdio.h>
#include <stdlib.h>

unsigned long program_0(unsigned long ram)
{
	/// <summary>
	/// performs temp = x !& y
	/// x is in register 2, y is in register 3, temp is in register 4
	/// </summary>
	register unsigned long a, b, res;
	a = (ram >> 2) & 1UL; // must have & 1UL at end of every operation to get values to isolate wanted value
	b = (ram >> 3) & 1UL;
	res = !(a & b) & 1UL;
	ram = (ram & ~(1UL << 4)) | (res << 4) // stores value of res in register 4
}
int main(int argc, char* argv[])
{
	if (argc < 3) 
	{
		printf("error: not enough inputs");
		return 0;
	}
	register unsigned long ram = 1UL << 1; // register 0 and 1 are reserved for true, false

	if (argv[1][0] == '1') ram |= (1UL << 2); // register 2 and 3 are reserved for inputs
	if (argv[2][0] == '1') ram |= (1UL << 3);
	ram = program_0(ram);
	return 0;
}