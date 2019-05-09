#include <stdio.h>
namespace Weird {

	struct IFoo {
		virtual int A(void *, IFoo **) = 0;
		virtual int B() = 0;
		virtual int C() = 0;


		virtual void Test(int arg, char *arg2) = 0;
	};

	class Foo : public IFoo {
	public:
		void Test(int arg, char *arg2) {
			for (int c = 0; c < arg; c++)
				printf("%s\n", arg2);
		}

		int A(void *, IFoo **ret) {
			*ret = this;
			return 0;
		}

		int B() {
			return 0;
		}

		int C() {
			return 0;
		}
	};

	extern "C" IFoo *CreateWeirdObject() {
		return new Foo();
	}
}