#include "com.h"

struct ITest;

COMINTERFACE(ITestCallback, 073738b9, e093, 4482, 8e, c0, e5, d9, 25, 4c, c7, eb) : IUnknown
{
	virtual HRESULT CallMe(char* text, ITest* caller) = 0;
};

COMINTERFACE(ITest, 0565100c, a798, 4909, 9b, f4, d4, 2c, aa,b9, e7, 34) : IUnknown
{
	virtual HRESULT Test(int arg, char *arg2) = 0;
	virtual HRESULT SetCallback(ITestCallback* cb) = 0;
	virtual HRESULT DoCallback(char* text) = 0;
};









struct VTable
{
	void*Methods[256];
};