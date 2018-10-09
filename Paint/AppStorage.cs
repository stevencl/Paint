// =====================================================================
//
//  This file is part of the Microsoft Dynamics CRM SDK code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//
// =====================================================================
using System;
using System.IO.IsolatedStorage;

namespace TestPhoneApplication
{
	public class AppStorage
	{
		private static AppStorage _instance;




		public static AppStorage Instance
		{
			get
			{
				if (_instance == null)
				{
					if (IsolatedStorageSettings.ApplicationSettings.TryGetValue("appStorage", out _instance) == false)
					{
						_instance = new AppStorage();
					}
				}
				return _instance;
			}
		}



	}
}
