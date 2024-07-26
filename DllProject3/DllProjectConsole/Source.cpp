
#include <windows.h>
#include <stdio.h>
#include <iostream>


/* Define a function pointer for our imported
 * function.
 * This reads as "introduce the new type f_funci as the type: 
 *                pointer to a function returning an int and 
 *                taking no arguments.
 *
 * Make sure to use matching calling convention (__cdecl, __stdcall, ...)
 * with the exported function. __stdcall is the convention used by the WinAPI
 */
typedef BOOL (__stdcall *f_helloworld)();

int main()
{
	LPCSTR path = "C:\\Users\\Stajyer\\Documents\\Visual Studio 2012\\Projects\\DllProject3\\Debug\\DllProject3.dll";
	HINSTANCE hGetProcIDDLL = LoadLibrary(path);

	  if (!hGetProcIDDLL) {
		std::cout << "could not load the dynamic library" << std::endl;
		return EXIT_FAILURE;
	  }

	  // resolve function address here
	  f_helloworld helloworld = (f_helloworld)GetProcAddress(hGetProcIDDLL, "HelloWorld");
	  if (!helloworld) {
		std::cout << "could not locate the function" << std::endl;
		return EXIT_FAILURE;
	  }

	  std::cout << "helloworld() returned " << helloworld() << std::endl;
	  scanf("%s");
	  return EXIT_SUCCESS;
}