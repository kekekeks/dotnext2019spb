#include <stdio.h>
#include <cstring>

namespace Complete {


	typedef struct _GUID {
		unsigned int  Data1;
		unsigned short Data2;
		unsigned short Data3;
		unsigned char  Data4[ 8 ];
	} GUID;

	#define E_NOINTERFACE 0x80004002L
	const GUID IID_IUnknown = {0x0, 0x0, 0x0, {0xC0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 } };
	const GUID IID_IFoo = {0xb48e6d94, 0xf943, 0x42cd, {0x91, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81, 0x81 } };


	struct IFoo {
		virtual int QueryInterface(GUID*, void**) = 0;
		virtual int AddRef() = 0;
		virtual int Release() = 0;


		virtual void Test(int arg, char *arg2) = 0;
	};

	class Foo : public IFoo {
	private:
		int _ref = 1;
	public:
		void Test(int arg, char *arg2) {
			for (int c = 0; c < arg; c++)
				printf("%s\n", arg2);
		}

		int QueryInterface(GUID*riid, void**ppv) {
			if(0 == memcmp(riid, &IID_IUnknown, sizeof(GUID))
			        || memcmp(riid, &IID_IFoo, sizeof(GUID))) {
				AddRef();
				*ppv = (IFoo *) this;
				return 0;
			}
			return E_NOINTERFACE;
		}

		int AddRef() {
			_ref++;
			return _ref;
		}

		int Release() {
			_ref--;
			int rv = _ref;
			if(_ref == 0)
				delete this;
			return rv;
		}

		~Foo()
		{
			printf("Deleted\n");
		}
	};

	extern "C" IFoo *CreateCompleteObject() {
		return new Foo();
	}
}