#include <stdio.h>
#include "include/shared.h"
#include "include/comimpl.h"

namespace Callback {

	char* concat(const char *s1, const char *s2)
	{
		char *result = new char[strlen(s1) + strlen(s2) + 1];
		strcpy(result, s1);
		strcat(result, s2);
		return result;
	}

	class CallbackTest : public ComSingleObject<ITest, &IID_ITest> {
	private:
		ComPtr<ITestCallback> _cb;
	public:
FORWARD_IUNKNOWN()


		virtual HRESULT Test(int arg, char *arg2) override {
			for (int c = 0; c < arg; c++)
				printf("%s\n", arg2);
			return S_OK;
		}

		virtual HRESULT SetCallback(ITestCallback* cb) override
		{
			_cb = cb;
			return S_OK;
		}

		virtual HRESULT DoCallback(char* text) override
		{
			if(_cb == nullptr)
				return E_FAIL;
			if(text == nullptr)
				return E_POINTER;
			char* test = concat("Echo: ", text);
			_cb->CallMe(test, this);
			delete test;
			return S_OK;
		}
	};

	extern "C" ITest* CreateCallbackObject() {
		return  (ITest*)new CallbackTest();
	}
}









//		auto vt = **(void***)(void*)rv;