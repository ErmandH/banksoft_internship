#include <Windows.h>

#ifndef MYLIBRARY_H
#define MYLIBRARY_H

#ifdef MYLIBRARY_EXPORTS
#define MYLIBRARY_API __declspec(dllexport)
#else
#define MYLIBRARY_API __declspec(dllimport)
#endif

extern "C" MYLIBRARY_API BOOL WINAPI HelloWorld();

#endif // MYLIBRARY_H
