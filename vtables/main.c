typedef struct IFoo IFoo;
int MySuperFunction(int, char* );
struct IFoo
{
	void (*Test)(IFoo** self, int arg, char* arg2);
};

extern void* CreateSimpleObject();

int main()
{
	IFoo** x = (IFoo**)CreateSimpleObject();
	(*x)->Test(x, 2, "Hello world!");

	return 0;
}