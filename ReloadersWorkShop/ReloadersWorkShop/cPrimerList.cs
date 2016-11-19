﻿//============================================================================*
// cPrimerList.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

//============================================================================*
// Namespace
//============================================================================*

namespace ReloadersWorkShop
	{
	[Serializable]
	public class cPrimerList : List<cPrimer>
		{
		//============================================================================*
		// AddPrimer()
		//============================================================================*

		public bool AddPrimer(cPrimer Primer)
			{
			foreach (cPrimer CheckPrimer in this)
				{
				if (CheckPrimer.CompareTo(Primer) == 0)
					return (false);
				}

			Add(Primer);

			return (true);
			}

		//============================================================================*
		// Export()
		//============================================================================*

		public void Export(StreamWriter Writer)
			{
			if (Count <= 0)
				return;

			string strLine = "";

			Writer.WriteLine(cPrimer.CSVHeader);
			Writer.WriteLine();

			Writer.WriteLine(cPrimer.CSVLineHeader);
			Writer.WriteLine();

			foreach (cPrimer Primer in this)
				{
				strLine = Primer.CSVLine;

				Writer.WriteLine(strLine);
				}

			Writer.WriteLine();
			}

		//============================================================================*
		// Export()
		//============================================================================*

		public void Export(XmlDocument XMLDocument, XmlElement XMLParentElement)
			{
			if (Count > 0)
				{
				XmlElement XMLElement = XMLDocument.CreateElement(string.Empty, "Primers", string.Empty);
				XMLParentElement.AppendChild(XMLElement);

				foreach (cPrimer Primer in this)
					Primer.Export(XMLDocument, XMLElement);
				}
			}

		//============================================================================*
		// HandgunCount Property
		//============================================================================*

		public int HandgunCount
			{
			get
				{
				int nCount = 0;

				foreach (cPrimer Primer in this)
					{
					if (Primer.FirearmType == cFirearm.eFireArmType.Handgun)
						nCount++;
					}

				return (nCount);
				}
			}

		//============================================================================*
		// RecalulateInventory()
		//============================================================================*

		public void RecalulateInventory(cDataFiles DataFiles)
			{
			foreach (cSupply Supply in this)
				Supply.RecalculateInventory(DataFiles);
			}

		//============================================================================*
		// RifleCount Property
		//============================================================================*

		public int RifleCount
			{
			get
				{
				int nCount = 0;

				foreach (cPrimer Primer in this)
					{
					if (Primer.FirearmType == cFirearm.eFireArmType.Rifle)
						nCount++;
					}

				return (nCount);
				}
			}
		}
	}
