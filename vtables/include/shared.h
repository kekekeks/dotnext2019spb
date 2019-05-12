#include "com.h"

struct IUtf8String;
struct ITest;

COMINTERFACE(ITestCallback, 073738b9, e093, 4482, 8e, c0, e5, d9, 25, 4c, c7, eb) : IUnknown
{
	virtual HRESULT STDMETHODCALLTYPE CallMe(char* text, ITest* caller) = 0;
};



COMINTERFACE(ITest, 0565100c, a798, 4909, 9b, f4, d4, 2c, aa,b9, e7, 34) : IUnknown
{
	virtual HRESULT STDMETHODCALLTYPE Test(int arg, char *arg2) = 0;
	virtual HRESULT STDMETHODCALLTYPE SetCallback(ITestCallback* cb) = 0;
	virtual HRESULT STDMETHODCALLTYPE DoCallback(char* text) = 0;
};
































COMINTERFACE(IUtf8String, fca2c459, 0818, 4cb4, 8d, bd, fd, 98, 4b, cc,a5, 6a) : IUnknown
{
	virtual int GetLength() = 0;
	virtual const char* GetData() = 0;
};

COMINTERFACE(INativeFactory, 4ff9f886, 3292, 4191, b9, 31, d0, eb, bc, d1, 2a, fe) : IUnknown
{
	virtual HRESULT STDMETHODCALLTYPE CreateTest(ITest** ppv) = 0;
	virtual HRESULT STDMETHODCALLTYPE Concat(IUtf8String* s1, IUtf8String* s2, IUtf8String** ppv) = 0;
};


extern "C" INativeFactory* CreateNativeFactory();

