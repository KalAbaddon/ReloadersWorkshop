﻿//============================================================================*
// cBatch.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.IO;
using System.Xml;

//============================================================================*
// Application Specific Using Statements
//============================================================================*

using ReloadersWorkShop.Preferences;

//============================================================================*
// NameSpace
//============================================================================*

namespace ReloadersWorkShop
	{
	//============================================================================*
	// cBatch class
	//============================================================================*

	[Serializable]
	public class cBatch : IComparable
		{
		//============================================================================*
		// Private Data Members
		//============================================================================*

		//----------------------------------------------------------------------------*
		// General Data
		//----------------------------------------------------------------------------*

		private int m_nBatchID = 0;
		private string m_strUserID = "";

		private DateTime m_DateLoaded = DateTime.Today;

		private cLoad m_Load = null;
		private cFirearm m_Firearm = null;
		private double m_dPowderWeight = 0.0;

		private bool m_fTrackInventory = false;

		//----------------------------------------------------------------------------*
		// Batch Details
		//----------------------------------------------------------------------------*

		private int m_nNumRounds = 0;
		private int m_nTimesFired = 0;

		private double m_dCOL = 0.0;
		private double m_dCBTO = 0.0;
		private double m_dHeadSpace = 0.0;
		private double m_dNeckSize = 0.0;
		private double m_dNeckWall = 0.0;
		private double m_dCaseTrimLength = 0.0;
		private double m_dBulletDiameter = 0.0;

		private bool m_fFullLengthSized = false;
		private bool m_fNeckSized = false;
		private bool m_fExpandedNeck = false;

		private bool m_fNeckTurned = false;
		private bool m_fAnnealed = false;
		private bool m_fModifiedBullet = false;

		private bool m_fJumpSet = false;
		private double m_dJump = 0.0;

		private cBatchTest m_BatchTest = null;

		//----------------------------------------------------------------------------*
		// Miscellaneous
		//----------------------------------------------------------------------------*

		private bool m_fArchive = false;
		private bool m_fChecked = false;
		private int m_nNumPrinted = 0;

		//============================================================================*
		// cBatch() - Constructor
		//============================================================================*

		public cBatch()
			{
			}

		//============================================================================*
		// cBatch() - Copy Constructor
		//============================================================================*

		public cBatch(cBatch Batch)
			{
			Copy(Batch);
			}

		//============================================================================*
		// Annealed Property
		//============================================================================*

		public bool Annealed
			{
			get
				{
				return (m_fAnnealed);
				}
			set
				{
				m_fAnnealed = value;
				}
			}

		//============================================================================*
		// Archived Property
		//============================================================================*

		public bool Archived
			{
			get
				{
				return (m_fArchive);
				}
			set
				{
				m_fArchive = value;
				}
			}

		//============================================================================*
		// BatchID Property
		//============================================================================*

		public int BatchID
			{
			get
				{
				return (m_nBatchID);
				}
			set
				{
				m_nBatchID = value;
				}
			}

		//============================================================================*
		// BatchCost Property
		//============================================================================*

		public double BatchCost
			{
			get
				{
				double dCost = 0.0;

				if (Load != null)
					{
					dCost += (Load.Bullet.CostEach * m_nNumRounds);
					dCost += (Load.Case.CostEach * m_nNumRounds);
					dCost += (Load.Primer.CostEach * m_nNumRounds);
					dCost += ((m_dPowderWeight * Load.Powder.CostEach) * m_nNumRounds);
					}

				return (dCost);
				}
			}

		//============================================================================*
		// BatchTest Property
		//============================================================================*

		public cBatchTest BatchTest
			{
			get
				{
				return (m_BatchTest);
				}
			set
				{
				m_BatchTest = value;
				}
			}

		//============================================================================*
		// BulletDiameter Property
		//============================================================================*

		public double BulletDiameter
			{
			get
				{
				return (m_dBulletDiameter);
				}
			set
				{
				m_dBulletDiameter = value;
				}
			}

		//============================================================================*
		// CartridgeCost Property
		//============================================================================*

		public double CartridgeCost
			{
			get
				{
				double dBulletCost = 0.0;
				double dCaseCost = 0.0;
				double dPowderCost = 0.0;
				double dPrimerCost = 0.0;

				if (Load != null && Load.Bullet != null)
					{
					dBulletCost = Load.Bullet.CostEach;

					dCaseCost = Load.Case.CostEach;

					if (m_nTimesFired > 0)
						dCaseCost = dCaseCost / (double) ((double) m_nTimesFired + 1.0);

					dPowderCost = Load.Powder.CostEach * (m_dPowderWeight / (cPreferences.MetricCanWeights ? 1000.0 : 7000.0));

					dPrimerCost = Load.Primer.CostEach;
					}

				return (dBulletCost + dCaseCost + dPowderCost + dPrimerCost);
				}
			}

		//============================================================================*
		// CaseTrimLength Property
		//============================================================================*

		public double CaseTrimLength
			{
			get
				{
				return (m_dCaseTrimLength);
				}
			set
				{
				m_dCaseTrimLength = value;
				}
			}

		//============================================================================*
		// Checked Property
		//============================================================================*

		public bool Checked
			{
			get
				{
				return (m_fChecked);
				}
			set
				{
				m_fChecked = value;
				}
			}

		//============================================================================*
		// COL Property
		//============================================================================*

		public double COL
			{
			get
				{
				return (m_dCOL);
				}
			set
				{
				m_dCOL = value;
				}
			}

		//============================================================================*
		// Comparer()
		//============================================================================*

		public static int Comparer(cBatch Batch1, cBatch Batch2)
			{
			if (Batch1 == null)
				{
				if (Batch2 != null)
					return (-1);
				else
					return (0);
				}
			else
				{
				if (Batch2 == null)
					return (1);
				}

			return (Batch1.CompareTo(Batch2));
			}

		//============================================================================*
		// CompareTo()
		//============================================================================*

		public int CompareTo(Object obj)
			{
			if (obj == null)
				return (1);

			cBatch Batch = (cBatch) obj;

			//----------------------------------------------------------------------------*
			// Batch ID
			//----------------------------------------------------------------------------*

			return (m_nBatchID.CompareTo(Batch.m_nBatchID));
			}

		//============================================================================*
		// Copy()
		//============================================================================*

		public void Copy(cBatch Batch)
			{
			m_nBatchID = Batch.m_nBatchID;
			m_strUserID = Batch.m_strUserID;
			m_DateLoaded = new DateTime(Batch.DateLoaded.Ticks);
			m_dPowderWeight = Batch.m_dPowderWeight;
			m_nNumRounds = Batch.m_nNumRounds;
			m_nTimesFired = Batch.m_nTimesFired;
			m_dCOL = Batch.m_dCOL;
			m_dCBTO = Batch.m_dCBTO;
			m_dHeadSpace = Batch.m_dHeadSpace;
			m_dNeckSize = Batch.m_dNeckSize;
			m_dNeckWall = Batch.m_dNeckWall;
			m_dCaseTrimLength = Batch.m_dCaseTrimLength;
			m_dBulletDiameter = Batch.m_dBulletDiameter;

			m_fFullLengthSized = Batch.m_fFullLengthSized;
			m_fNeckSized = Batch.m_fNeckSized;
			m_fExpandedNeck = Batch.m_fExpandedNeck;

			m_fNeckTurned = Batch.m_fNeckTurned;
			m_fAnnealed = Batch.m_fAnnealed;
			m_fModifiedBullet = Batch.m_fModifiedBullet;

			m_fJumpSet = Batch.m_fJumpSet;
			m_dJump = Batch.m_dJump;

			m_Firearm = Batch.m_Firearm;

			m_Load = Batch.m_Load;

			m_BatchTest = Batch.BatchTest;

			m_fArchive = Batch.m_fArchive;

			m_fTrackInventory = Batch.m_fTrackInventory;
			}

		//============================================================================*
		// Cost Property
		//============================================================================*

		public double Cost
			{
			get
				{
				return (CartridgeCost * m_nNumRounds);
				}
			}

		//============================================================================*
		// CSVHeader Property
		//============================================================================*

		public static string CSVHeader
			{
			get
				{
				return ("Batches");
				}
			}

		//============================================================================*
		// CSVLine Property
		//============================================================================*

		public string CSVLine
			{
			get
				{
				string strLine = "";

				strLine += m_nBatchID;
				strLine += ",";
				strLine += m_strUserID;
				strLine += ",";

				strLine += m_DateLoaded.ToShortDateString();
				strLine += ",";

				strLine += m_Load.Bullet.ToString();
				strLine += ",";
				strLine += m_Load.Powder.ToString();
				strLine += ",";
				strLine += m_Load.Primer.ToString();
				strLine += ",";
				strLine += m_Load.Case.ToString();
				strLine += ",";
				strLine += m_Firearm;
				strLine += ",";
				strLine += m_dPowderWeight;
				strLine += ",";

				strLine += m_nNumRounds;
				strLine += ",";
				strLine += m_nTimesFired;
				strLine += ",";

				strLine += m_dCOL;
				strLine += ",";
				strLine += m_dCBTO;
				strLine += ",";
				strLine += m_dHeadSpace;
				strLine += ",";
				strLine += m_dNeckSize;
				strLine += ",";
				strLine += m_dNeckWall;
				strLine += ",";
				strLine += m_dCaseTrimLength;
				strLine += ",";
				strLine += m_dBulletDiameter;
				strLine += ",";

				strLine += m_fFullLengthSized ? "Yes," : "-,";
				strLine += m_fNeckSized ? "Yes," : "-,";
				strLine += m_fExpandedNeck ? "Yes," : "-,";
				strLine += m_fNeckTurned ? "Yes," : "-,";
				strLine += m_fAnnealed ? "Yes," : "-,";
				strLine += m_fModifiedBullet ? "Yes," : "-,";

				strLine += m_fJumpSet ? "Yes," : "-,";
				strLine += m_dJump;
				strLine += ",";

				strLine += m_fArchive ? "Yes" : "-";

				return (strLine);
				}
			}

		//============================================================================*
		// CSVLineHeader Property
		//============================================================================*

		public static string CSVLineHeader
			{
			get
				{
				string strLine = "Batch ID,User Batch ID,Date Loaded,Bullet,Powder,Primer,Case,Firearm,Powder Weight,Num Rounds,Times Case Fired,COAL,CBTO,Headspace,Neck Size,Neck Wall,";
				strLine += "Case Trim Length,Bullet Diameter,Full-Length Sized,Neck Sized,Expanded Neck,Nect Turned,Annealed,Modified Bullet,Jumpset,Jump,Archived";

				return (strLine);
				}
			}

		//============================================================================*
		// DateLoaded Property
		//============================================================================*

		public DateTime DateLoaded
			{
			get
				{
				return (m_DateLoaded);
				}
			set
				{
				m_DateLoaded = value;
				}
			}

		//============================================================================*
		// ExpandedNeck Property
		//============================================================================*

		public bool ExpandedNeck
			{
			get
				{
				return (m_fExpandedNeck);
				}
			set
				{
				m_fExpandedNeck = value;
				}
			}

		//============================================================================*
		// Export() - CSV
		//============================================================================*

		public void Export(StreamWriter Writer, bool fIncludeTests = true)
			{
			Writer.WriteLine(CSVLine);

			if (fIncludeTests && m_BatchTest != null)
				{
				Writer.WriteLine();

				m_BatchTest.Export(Writer);
				}
			}

		//============================================================================*
		// Export() - XML Document
		//============================================================================*

		public void Export(XmlDocument XMLDocument, XmlElement XMLParentElement, bool fIncludeTests = true)
			{
			XmlElement XMLThisElement = XMLDocument.CreateElement("Batch");
			XMLParentElement.AppendChild(XMLThisElement);

			// Batch ID

			XmlElement XMLElement = XMLDocument.CreateElement("BatchID");
			XmlText XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_nBatchID));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// User Batch ID

			if (!String.IsNullOrEmpty(m_strUserID))
				{
				XMLElement = XMLDocument.CreateElement("UserBatchID");
				XMLTextElement = XMLDocument.CreateTextNode(m_strUserID);
				XMLElement.AppendChild(XMLTextElement);

				XMLThisElement.AppendChild(XMLElement);
				}

			// Date Loaded

			XMLElement = XMLDocument.CreateElement("DateLoaded");
			XMLTextElement = XMLDocument.CreateTextNode(m_DateLoaded.ToShortDateString());
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Bullet

			XMLElement = XMLDocument.CreateElement("Bullet");
			XMLTextElement = XMLDocument.CreateTextNode(m_Load.Bullet.ToString());
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Powder

			XMLElement = XMLDocument.CreateElement("Powder");
			XMLTextElement = XMLDocument.CreateTextNode(m_Load.Powder.ToString());
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Primer

			XMLElement = XMLDocument.CreateElement("Primer");
			XMLTextElement = XMLDocument.CreateTextNode(m_Load.Primer.ToString());
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Case

			XMLElement = XMLDocument.CreateElement("Case");
			XMLTextElement = XMLDocument.CreateTextNode(m_Load.Case.ToString());
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Firearm

			if (m_Firearm != null)
				{
				XMLElement = XMLDocument.CreateElement("Firearm");
				XMLTextElement = XMLDocument.CreateTextNode(m_Firearm.ToString());
				XMLElement.AppendChild(XMLTextElement);

				XMLThisElement.AppendChild(XMLElement);
				}

			// Powder Weight

			XMLElement = XMLDocument.CreateElement("PowderWeight");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dPowderWeight));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Num Rounds

			XMLElement = XMLDocument.CreateElement("NumRounds");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_nNumRounds));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Cases Fired

			XMLElement = XMLDocument.CreateElement("CasesFired");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_nTimesFired));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// COAL

			XMLElement = XMLDocument.CreateElement("COAL");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dCOL));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// CBTO

			XMLElement = XMLDocument.CreateElement("CBTO");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dCBTO));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Headspace

			XMLElement = XMLDocument.CreateElement("Headspace");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dHeadSpace));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Neck Size

			XMLElement = XMLDocument.CreateElement("NeckSize");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dNeckSize));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Neck Wall

			XMLElement = XMLDocument.CreateElement("NeckWall");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dNeckWall));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Case Trim Length

			XMLElement = XMLDocument.CreateElement("CaseTrimLength");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dCaseTrimLength));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Bullet Diameter

			XMLElement = XMLDocument.CreateElement("BulletDiameter");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dBulletDiameter));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Full Length Sized

			XMLElement = XMLDocument.CreateElement("FullLengthSized");
			XMLTextElement = XMLDocument.CreateTextNode(m_fFullLengthSized ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Neck Sized

			XMLElement = XMLDocument.CreateElement("NeckSized");
			XMLTextElement = XMLDocument.CreateTextNode(m_fNeckSized ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Expanded Neck

			XMLElement = XMLDocument.CreateElement("ExpandedNeck");
			XMLTextElement = XMLDocument.CreateTextNode(m_fExpandedNeck ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Neck Turned

			XMLElement = XMLDocument.CreateElement("NeckTurned");
			XMLTextElement = XMLDocument.CreateTextNode(m_fNeckTurned ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Annealed

			XMLElement = XMLDocument.CreateElement("Annealed");
			XMLTextElement = XMLDocument.CreateTextNode(m_fAnnealed ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Modified Bullet

			XMLElement = XMLDocument.CreateElement("ModifiedBullet");
			XMLTextElement = XMLDocument.CreateTextNode(m_fModifiedBullet ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Jump Set

			XMLElement = XMLDocument.CreateElement("JumpSet");
			XMLTextElement = XMLDocument.CreateTextNode(m_fJumpSet ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Jump

			XMLElement = XMLDocument.CreateElement("Jump");
			XMLTextElement = XMLDocument.CreateTextNode(String.Format("{0}", m_dJump));
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			// Archived

			XMLElement = XMLDocument.CreateElement("Archived");
			XMLTextElement = XMLDocument.CreateTextNode(m_fArchive ? "Yes" : "-");
			XMLElement.AppendChild(XMLTextElement);

			XMLThisElement.AppendChild(XMLElement);

			if (m_BatchTest != null && fIncludeTests)
				m_BatchTest.Export(XMLDocument, XMLThisElement);
			}

		//============================================================================*
		// Firearm Property
		//============================================================================*

		public cFirearm Firearm
			{
			get
				{
				return (m_Firearm);
				}
			set
				{
				m_Firearm = value;
				}
			}

		//============================================================================*
		// FullLengthSized Property
		//============================================================================*

		public bool FullLengthSized
			{
			get
				{
				return (m_fFullLengthSized);
				}
			set
				{
				m_fFullLengthSized = value;
				}
			}

		//============================================================================*
		// HeadSpace Property
		//============================================================================*

		public double HeadSpace
			{
			get
				{
				return (m_dHeadSpace);
				}
			set
				{
				m_dHeadSpace = value;
				}
			}

		//============================================================================*
		// Jump Property
		//============================================================================*

		public double Jump
			{
			get
				{
				return (m_dJump);
				}
			set
				{
				m_dJump = value;
				}
			}

		//============================================================================*
		// JumpSet Property
		//============================================================================*

		public bool JumpSet
			{
			get
				{
				return (m_fJumpSet);
				}
			set
				{
				m_fJumpSet = value;
				}
			}

		//============================================================================*
		// Load Property
		//============================================================================*

		public cLoad Load
			{
			get
				{
				return (m_Load);
				}
			set
				{
				m_Load = value;
				}
			}

		//============================================================================*
		// ModifiedBullet Property
		//============================================================================*

		public bool ModifiedBullet
			{
			get
				{
				return (m_fModifiedBullet);
				}
			set
				{
				m_fModifiedBullet = value;
				}
			}

		//============================================================================*
		// NeckSize Property
		//============================================================================*

		public double NeckSize
			{
			get
				{
				return (m_dNeckSize);
				}
			set
				{
				m_dNeckSize = value;
				}
			}

		//============================================================================*
		// NeckSized Property
		//============================================================================*

		public bool NeckSized
			{
			get
				{
				return (m_fNeckSized);
				}
			set
				{
				m_fNeckSized = value;
				}
			}

		//============================================================================*
		// NeckTurned Property
		//============================================================================*

		public bool NeckTurned
			{
			get
				{
				return (m_fNeckTurned);
				}
			set
				{
				m_fNeckTurned = value;
				}
			}

		//============================================================================*
		// NeckWall Property
		//============================================================================*

		public double NeckWall
			{
			get
				{
				return (m_dNeckWall);
				}
			set
				{
				m_dNeckWall = value;
				}
			}

		//============================================================================*
		// NumPrinted Property
		//============================================================================*

		public int NumPrinted
			{
			get
				{
				return (m_nNumPrinted);
				}
			set
				{
				m_nNumPrinted = value;
				}
			}

		//============================================================================*
		// NumRounds Property
		//============================================================================*

		public int NumRounds
			{
			get
				{
				return (m_nNumRounds);
				}
			set
				{
				m_nNumRounds = value;
				}
			}

		//============================================================================*
		// CBTO Property
		//============================================================================*

		public double CBTO
			{
			get
				{
				return (m_dCBTO);
				}
			set
				{
				m_dCBTO = value;
				}
			}

		//============================================================================*
		// PowderWeight Property
		//============================================================================*

		public double PowderWeight
			{
			get
				{
				return (m_dPowderWeight);
				}
			set
				{
				m_dPowderWeight = value;
				}
			}

		//============================================================================*
		// Synch() - Batch
		//============================================================================*

		public bool Synch()
			{
			if (m_BatchTest != null)
				m_BatchTest.Batch = this;

			return (true);
			}

		//============================================================================*
		// Synch() - Load
		//============================================================================*

		public bool Synch(cLoad Load)
			{
			if (m_Load != null && m_Load.CompareTo(Load) == 0)
				{
				m_Load = Load;

				return (true);
				}

			return (false);
			}

		//============================================================================*
		// TimesFired Property
		//============================================================================*

		public int TimesFired
			{
			get
				{
				return (m_nTimesFired);
				}
			set
				{
				m_nTimesFired = value;
				}
			}

		//============================================================================*
		// ToString()
		//============================================================================*

		public override string ToString()
			{
			string strLoadString = String.Format("Batch #{0:N0} - {1} Bullet with {2:F1} grs of {3} Powder", m_nBatchID, m_Load.Bullet.ToString(), m_dPowderWeight, m_Load.Powder.ToString());

			return (strLoadString);
			}

		//============================================================================*
		// TrackInventory Property
		//============================================================================*

		public bool TrackInventory
			{
			get
				{
				return (m_fTrackInventory);
				}
			set
				{
				m_fTrackInventory = value;
				}
			}

		//============================================================================*
		// UserID Property
		//============================================================================*

		public string UserID
			{
			get
				{
				return (m_strUserID);
				}
			set
				{
				m_strUserID = value;
				}
			}

		//============================================================================*
		// XMLHeader Property
		//============================================================================*

		public static string XMLHeader
			{
			get
				{
				return ("Batches");
				}
			}

		//============================================================================*
		// XMLLine Property
		//============================================================================*

		public string XMLLine
			{
			get
				{
				string strLine = "";


				return (strLine);
				}
			}

		//============================================================================*
		// XMLLineHeader Property
		//============================================================================*

		public static string XMLLineHeader
			{
			get
				{
				string strLine = "Firearm Type,Name,Headstamp,Handgun Type,Small Primer,Large Primer,Magnum Primer,Min Bullet Dia.,Max Bullet Dia.,Min Bullet Weight,Max Bullet Weight,Case Trim Length,Max Case Length,Max COAL,Max Neck Dia";

				return (strLine);
				}
			}
		}
	}
