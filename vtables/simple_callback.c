
typedef void (*MyCallback)(int);
static MyCallback callback;
extern void SetCCallback(MyCallback cb)
{
	callback = cb;
}

void SetCallback(MyCallback cb, void* userData)
{

}

extern void CallCCallback(int arg)
{
	callback(arg);
}