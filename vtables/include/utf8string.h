//
// Created by kekekeks on 12.05.19.
//

#ifndef DEMO_UTF8STRING_H
#define DEMO_UTF8STRING_H

#include "shared.h"
#include "comimpl.h"

class Utf8String : public ComSingleObject<IUtf8String, &IID_IUtf8String>
{
	int _length;
	char* _data;
public:
	FORWARD_IUNKNOWN();

	char* CStr()
	{
		return _data;
	}

	virtual int GetLength() override
	{
		return _length;
	}

	virtual const char* GetData() override {
		return _data;
	}

	Utf8String(int len)
	{
		_length = len;
		_data = new char[len + 1];
		memset(_data, 0, len + 1);
	}

	Utf8String(char* s, int len)
	{
		_length = len;
		_data = new char[len + 1];
		memcpy(_data, s, len);
		_data[len] = 0;
	}

	Utf8String(char* s) : Utf8String(s, strlen(s))
	{

	}
};

#endif //DEMO_UTF8STRING_H
