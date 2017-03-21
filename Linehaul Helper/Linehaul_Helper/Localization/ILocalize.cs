using System;
using System.Globalization;

namespace Linehaul_Helper.Localization
{
	public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();
		void SetLocale(CultureInfo ci);
	}
}
