#include "include/utf8string.h"



extern "C" ITest* CreateCallbackObject();
class Factory : public ComSingleObject<INativeFactory, &IID_INativeFactory>
{
public:
	FORWARD_IUNKNOWN()
	virtual HRESULT STDMETHODCALLTYPE Concat(IUtf8String* s1, IUtf8String* s2, IUtf8String** rv) override
	{
		if(s1== nullptr || s2 == nullptr)
			return E_POINTER;
		auto ret = new Utf8String(s1->GetLength() + s2->GetLength());
		int s1l = s1->GetLength();
		int s2l = s2->GetLength();


		if(s1l > 0) {
			memcpy(ret->CStr(), s1->GetData(), s1l);
		}
		if(s2l > 0)
			memcpy(ret->CStr() + s1l, s2->GetData(), s2l);
		*rv = (IUtf8String*)ret;
		return S_OK;
	}

	virtual HRESULT STDMETHODCALLTYPE CreateTest(ITest** rv)
	{
		*rv = CreateCallbackObject();
		return S_OK;
	}

};


extern "C" INativeFactory* CreateNativeFactory()
{
	return new Factory();
}

