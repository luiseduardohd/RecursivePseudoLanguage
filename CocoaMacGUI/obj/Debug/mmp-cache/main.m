#include <xamarin/xamarin.h>
#import <AppKit/NSAlert.h>
#import <Foundation/NSDate.h>



extern "C" int xammac_setup ()
{
	xamarin_use_il_registrar = false;
	xamarin_new_refcount = TRUE;


	return 0;
}

