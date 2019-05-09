#include <stdio.h>
namespace Simple {
	struct IFoo {
		virtual void Test(int arg, char *arg2) = 0;
	};

	class Foo : public IFoo {
	public:
		void Test(int arg, char *arg2) {
			for (int c = 0; c < arg; c++)
				printf("%s\n", arg2);
		}
	};


	extern "C" void *CreateSimpleObject() {
		return (IFoo *) new Foo();
	}

}