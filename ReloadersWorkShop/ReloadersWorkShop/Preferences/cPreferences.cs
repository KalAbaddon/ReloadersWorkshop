﻿//============================================================================*
// cPreferences.cs
//
// Copyright © 2013-2014, Kevin S. Beebe
// All Rights Reserved
//============================================================================*

//============================================================================*
// .Net Using Statements
//============================================================================*

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using ReloadersWorkShop.Ballistics;

//============================================================================*
// NameSpace
//============================================================================*

namespace ReloadersWorkShop.Preferences
	{
	//============================================================================*
	// cPreferences class
	//============================================================================*

	[Serializable]
	public class cPreferences
		{
		//============================================================================*
		// Public Enumerations
		//============================================================================*

		public enum eApplicationListView
			{
			BatchListView = 0,
			BatchTestListView,
			BallisticsListView,
			BulletCalibersListView,
			CalibersListView,
			ChargeListView,
			ChargeTestListView,
			FirearmsListView,
			FirearmsBulletListView,
			LoadDataListView,
			ManufacturersListView,
			BulletSuppliesListView,
			CaseSuppliesListView,
			PowderSuppliesListView,
			PrimerSuppliesListView,
			BatchLoadListView,
			CopyChargeListView,
			TransactionsListView,
			AmmoListView,
			EvaluationListView,
			AmmoTestListView
			}

		//============================================================================*
		// Private Data Members
		//============================================================================*

		private bool m_fDev = false;

		// Last Tab Selected on Main Tab Control

		private string m_strLastMainTabSelected = null;

		// Window and Column Locations and Sizes

		private bool m_fMaximized = false;
		private Point m_MainFormLocation = new Point(0, 0);
		private Size m_MainFormSize = new Size(1024, 768);
		private cColumnList m_ColumnList = new cColumnList();

		// Ammo Tab Settings

		private bool m_fAmmoPrintAll = true;
		private bool m_fAmmoPrintChecked = false;
		private bool m_fAmmoPrintNonZero = false;
		private bool m_fAmmoPrintBelowStock = false;
		private bool m_fAmmoPrintFactoryOnly = false;

		// Ammo List Settings

		private cAmmo m_LastAmmo = null;
		private cAmmo m_LastAmmoSelected = null;

		private cCaliber m_LastAmmoCaliber = null;
		private cManufacturer m_LastAmmoManufacturer = null;

		private int m_nAmmoSortColumn = 0;
		private SortOrder m_AmmoSortOrder = SortOrder.Ascending;

		// Ammo List Preview Dialog Size and Location

		private bool m_fAmmoListPreviewMaximized;
		private Point m_AmmoListPreviewLocation = new Point(100, 100);
		private Size m_AmmoListPreviewSize = new Size(425, 550);

		// Ammo Test List Settings

		private int m_nAmmoTestSortColumn = 0;
		private SortOrder m_AmmoTestSortOrder = SortOrder.Ascending;

		// Backup Settings

		private bool m_fBackupOK = false;

		private bool m_fAutoBackup = false;
		private string m_strBackupFolder = @"C:\Users\Public\ReloadersWorkShop\Backup";
		private int m_nAutoSaveTime = 30000;
		private int m_nBackupKeepDays = 30;

		// Ballistics Settings

		private cBatch m_BallisticsBatch = null;
		private cBullet m_BallisticsBullet = null;
		private cCaliber m_BallisticsCaliber = null;
		private double m_BallisticsCharge = 0.0;
		private cFirearm m_BallisticsFirearm = null;
		private cFirearm.eFireArmType m_BallisticsFirearmType = cFirearm.eFireArmType.Rifle;
		private cLoad m_BallisticsLoad = null;

		private cBallistics m_BallisticsData = new cBallistics();

		private bool m_fShowApexMarker = true;
		private bool m_fShowDropChartRangeMarkers = true;
		private bool m_fShowGroundStrikeMarkers = true;
		private bool m_fShowTransonicMarkers = true;
		private bool m_fShowWindDriftRangeMarkers = true;
		private bool m_fBallisticsUseSF = false;

		// Ballistics Preview Dialog Size and Location

		private bool m_fBallisticsPreviewMaximized = false;
		private Point m_BallisticsPreviewLocation = new Point(100, 100);
		private Size m_BallisticsPreviewSize = new Size(425, 550);

		// Batch Form Settings

		private int m_nNextBatchID = 1;

		private cFirearm.eFireArmType m_BatchEditorFirearmType = cFirearm.eFireArmType.Handgun;
		private cCaliber m_BatchEditorCaliber = null;
		private cBullet m_BatchEditorBullet = null;
		private cPowder m_BatchEditorPowder = null;

		private bool m_fBatchEditorIgnoreInventory = false;

		// Batch List Settings

		private cBatch m_LastBatch = null;
		private cBatch m_LastBatchSelected = null;
		private cBatchTest m_LastBatchTest = null;

		private cCaliber m_LastBatchCaliberSelected = null;
		private cBullet m_LastBatchBulletSelected = null;
		private cPowder m_LastBatchPowderSelected = null;
		private cBatchTest m_LastBatchTestSelected = null;

		private int m_nBatchSortColumn = 0;
		private SortOrder m_BatchSortOrder = SortOrder.Ascending;

		// Batch Load List Settings

		private cLoad m_LastBatchLoadSelected = null;
		private cCaliber m_LastBatchLoadCaliberSelected = null;
		private cBullet m_LastBatchLoadBulletSelected = null;
		private cPowder m_LastBatchLoadPowderSelected = null;

		private int m_nBatchLoadSortColumn = 0;
		private SortOrder m_BatchLoadSortOrder = SortOrder.Ascending;

		// Batch Print Form Settings

		private int m_nBatchPrintPaper = 0;
		private int m_nBatchPrintStartLabel = 0;

		private int m_nIndentMultiplier = 30;

		// Bullet Caliber Form Settings

		private cCaliber m_LastBulletCaliber = null;
		private double m_dLastBulletCaliberCOL = 0.0;

		// Bullet Caliber List Settings

		private cBulletCaliber m_LastBulletCaliberSelected = null;

		private int m_nBulletCaliberSortColumn = 0;
		private SortOrder m_BulletCaliberSortOrder = SortOrder.Ascending;

		// Bullet Supply List Settings

		private cBullet m_LastBullet = null;
		private cBullet m_LastBulletSelected = null;

		private int m_nBulletSortColumn = 0;
		private SortOrder m_BulletSortOrder = SortOrder.Ascending;

		// Caliber List Settings

		private cCaliber m_LastCaliber = null;
		private cCaliber m_LastCaliberSelected = null;
		private int m_nCaliberSortColumn = 0;
		private SortOrder m_CaliberSortOrder = SortOrder.Ascending;

		// Case Supply List Settings

		private cCase m_LastCase = null;
		private cCase m_LastCaseSelected = null;

		private int m_nCaseSortColumn = 0;
		private SortOrder m_CaseSortOrder = SortOrder.Ascending;

		// Charge Test Form Settings

		private cChargeTest m_LastChargeTest = null;

		// Charge Form Settings

		private cCharge m_LastCharge = null;
		private cCharge m_LastChargeSelected = null;

		// Charge List Settings

		private int m_nChargeSortColumn = 0;
		private SortOrder m_ChargeSortOrder = SortOrder.Ascending;

		// Charge Test List Settings

		private int m_nChargeTestSortColumn = 0;
		private SortOrder m_ChargeTestSortOrder = SortOrder.Ascending;

		// Conversion Calculator

		private int m_nConversionDecimals = 3;

		// Copy Charge Form Settings

		private cLoad m_LastCopyLoadSelected = null;

		private int m_nCopyChargeSortColumn = 0;
		private SortOrder m_CopyChargeSortOrder = SortOrder.Ascending;

		// Cost Analysis Preview Dialog Size and Location

		private bool m_fCostAnalysisPreviewMaximized = false;
		private Point m_CostAnalysisPreviewLocation = new Point(100, 100);
		private Size m_CostAnalysisPreviewSize = new Size(425, 550);

		// Data Entry settings

		private bool m_fAutoCheck = false;
		private bool m_fAutoCheckNonZero = false;

		private bool m_fToolTips = true;
		private bool m_fShowArchivedBatches = false;

		private int m_nDateFormat = 0;
		private string m_strDateFormat = @"M/d/yyyy";

		// Evaluation List Settings

		private int m_nEvaluationSortColumn = 0;
		private SortOrder m_EvaluationSortOrder = SortOrder.Ascending;

		private Point m_EvaluationListLocation = new Point(0, 0);
		private Size m_EvaluationListSize = new Size(0, 0);

		// Firearm Form Settings

		private cFirearmBullet m_LastFirearmBulletSelected = null;

		private int m_nFirearmBulletSortColumn = 0;
		private SortOrder m_FirearmBulletSortOrder = SortOrder.Ascending;

		// Firearm List Settings

		private bool m_fFirearmPrintAll = true;
		private bool m_fFirearmPrintDetail = false;
		private bool m_fFirearmPrintSpecs = false;

		private cFirearm m_LastFirearm = null;
		private cFirearm m_LastFirearmSelected = null;

		private cFirearmBullet m_LastFirearmBullet = null;

		private int m_nFirearmSortColumn = 0;
		private SortOrder m_FirearmSortOrder = SortOrder.Ascending;

		// Firearm List Preview Dialog Size and Location

		private bool m_fFirearmListPreviewMaximized = false;
		private Point m_FirearmListPreviewLocation = new Point(100, 100);
		private Size m_FirearmListPreviewSize = new Size(425, 550);

		// Hide Unchecked Button States

		private bool m_fHideUncheckedCalibers = false;
		private bool m_fHideUncheckedSupplies = false;

		// Inventory Activity Settings

		private cTransaction.eTransactionType m_eLastActivityType = cTransaction.eTransactionType.SetStockLevel;
		private string m_strLastPurchaseSource = "";
		private string m_strLastAddStockReason = "";
		private string m_strLastReduceStockReason = "";
		private string m_strLastFiredLocation = "";
		private bool m_fShowBatchTransactions = true;
		private bool m_fShowArchivedTransactions = false;

		// Inventory Group Settings

		private string m_strCurrency = "$";
		private bool m_fTrackInventory = false;
		private bool m_fTrackReloads = false;
		private bool m_fUseLastPurchase = false;
		private bool m_fIncludeTaxShipping = false;

		private double m_dTaxRate = 0.0;

		// Load Data Form Settings

		private cLoad m_LastLoad = null;

		private cFirearm.eFireArmType m_LoadDataFirearmType = cFirearm.eFireArmType.Handgun;
		private cCaliber m_LastLoadDataCaliberSelected = null;
		private cBullet m_LastLoadDataBulletSelected = null;
		private cPowder m_LastLoadDataPowderSelected = null;

		private string m_strShareFilePath = @".\Share";

		// Load Data List Settings

		private cLoad m_LastLoadSelected = null;

		private int m_nLoadDataSortColumn = 0;
		private SortOrder m_LoadDataSortOrder = SortOrder.Ascending;

		// Manufacturer List Settings

		private cManufacturer m_LastManufacturerSelected = null;

		private int m_nManufacturerSortColumn = 0;
		private SortOrder m_ManufacturerSortOrder = SortOrder.Ascending;

		// Measurements Settings

		private bool m_fMetricBulletWeights = false;
		private bool m_fMetricCanWeights = false;
		private bool m_fMetricDimensions = false;
		private bool m_fMetricFirearms = false;
		private bool m_fMetricGroups = false;
		private bool m_fMetricPowderWeights = false;
		private bool m_fMetricRanges = false;
		private bool m_fMetricShotWeights = false;
		private bool m_fMetricVelocities = false;

		// Atmospheric Settings

		private bool m_fMetricAltitudes = false;
		private bool m_fMetricPressures = false;
		private bool m_fMetricTemperatures = false;

		// Decimals

		private int m_nBulletWeightDecimals = 1;
		private int m_nCanWeightDecimals = 0;
		private int m_nDimensionDecimals = 3;
		private int m_nFirearmDecimals = 2;
		private int m_nGroupDecimals = 2;
		private int m_nPowderWeightDecimals = 1;
		private int m_nShotWeightDecimals = 1;

		// Powder Supply List Settings

		private int m_nPowderSortColumn = 0;
		private SortOrder m_PowderSortOrder = SortOrder.Ascending;

		private cPowder m_LastPowder = null;
		private cPowder m_LastPowderSelected = null;

		// Primer Form Settings

		private cPrimer m_LastPrimer = null;

		// Primer Supply List Settings

		private cPrimer m_LastPrimerSelected = null;

		private int m_nPrimerSortColumn = 0;
		private SortOrder m_PrimerSortOrder = SortOrder.Ascending;

		// Shopping List Preview Dialog Size and Location

		private bool m_fShoppingListPreviewMaximized = false;
		private Point m_ShoppingListPreviewLocation = new Point(100, 100);
		private Size m_ShoppingListPreviewSize = new Size(425, 550);

		// Supply Tab Settings

		private cSupply.eSupplyTypes m_eLastSupplyTypeSelected = cSupply.eSupplyTypes.Bullets;

		private bool m_fSupplyPrintAll = true;
		private bool m_fSupplyPrintChecked = false;
		private bool m_fSupplyPrintNonZero = false;
		private bool m_fSupplyPrintBelowStock = false;

		// Target Calculator Colors

		private Color m_AimPointColor = Color.FromName("DarkGreen");
		private Color m_OffsetColor = Color.FromName("Red");
		private Color m_ShotColor = Color.FromName("White");

		private Color m_ReticleColor = Color.FromName("Black");

		private Color m_CalibrationForecolor = Color.FromName("Black");
		private Color m_CalibrationBackcolor = Color.FromName("Yellow");

		private Color m_ExtremesColor = Color.FromName("White");

		// Transaction Form Settings

		private cTransaction m_LastTransaction = null;

		// Transaction List Settings

		private cTransaction m_LastTransactionSelected = null;

		private int m_nTransactionSortColumn = 0;
		private SortOrder m_TransactionSortOrder = SortOrder.Ascending;

		//============================================================================*
		// cPreferences() - Constructor
		//============================================================================*

		public cPreferences()
			{
			BackupFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);

			BackupFolder = Directory.GetParent(BackupFolder).ToString();

			ShareFilePath = BackupFolder;

			BackupFolder = Path.Combine(BackupFolder, @"Reloader's WorkShop\Backup");
			ShareFilePath = Path.Combine(ShareFilePath, @"Reloader's WorkShop\Share");
			}

		//============================================================================*
		// AimPointColor Property
		//============================================================================*

		public Color AimPointColor
			{
			get
				{
				return (m_AimPointColor);
				}
			set
				{
				m_AimPointColor = value;
				}
			}

		//============================================================================*
		// AmmoListPreviewLocation Property
		//============================================================================*

		public Point AmmoListPreviewLocation
			{
			get
				{
				return (m_AmmoListPreviewLocation);
				}
			set
				{
				m_AmmoListPreviewLocation = value;
				}
			}

		//============================================================================*
		// AmmoListPreviewMaximized Property
		//============================================================================*

		public bool AmmoListPreviewMaximized
			{
			get
				{
				return (m_fAmmoListPreviewMaximized);
				}
			set
				{
				m_fAmmoListPreviewMaximized = value;
				}
			}

		//============================================================================*
		// AmmoListPreviewSize Property
		//============================================================================*

		public Size AmmoListPreviewSize
			{
			get
				{
				return (m_AmmoListPreviewSize);
				}
			set
				{
				m_AmmoListPreviewSize = value;
				}
			}

		//============================================================================*
		// AmmoPrintAll Property
		//============================================================================*

		public bool AmmoPrintAll
			{
			get
				{
				return (m_fAmmoPrintAll);
				}
			set
				{
				m_fAmmoPrintAll = value;
				}
			}

		//============================================================================*
		// AmmoPrintBelowStock Property
		//============================================================================*

		public bool AmmoPrintBelowStock
			{
			get
				{
				return (m_fAmmoPrintBelowStock);
				}
			set
				{
				m_fAmmoPrintBelowStock = value;
				}
			}

		//============================================================================*
		// AmmoPrintChecked Property
		//============================================================================*

		public bool AmmoPrintChecked
			{
			get
				{
				return (m_fAmmoPrintChecked);
				}
			set
				{
				m_fAmmoPrintChecked = value;
				}
			}

		//============================================================================*
		// AmmoPrintFactoryOnly Property
		//============================================================================*

		public bool AmmoPrintFactoryOnly
			{
			get
				{
				return (m_fAmmoPrintFactoryOnly);
				}
			set
				{
				m_fAmmoPrintFactoryOnly = value;
				}
			}

		//============================================================================*
		// AmmoPrintNonZero Property
		//============================================================================*

		public bool AmmoPrintNonZero
			{
			get
				{
				return (m_fAmmoPrintNonZero);
				}
			set
				{
				m_fAmmoPrintNonZero = value;
				}
			}

		//============================================================================*
		// AmmoSortColumn Property
		//============================================================================*

		public int AmmoSortColumn
			{
			get
				{
				return (m_nAmmoSortColumn);
				}
			set
				{
				m_nAmmoSortColumn = value;
				}
			}

		//============================================================================*
		// AmmoSortOrder Property
		//============================================================================*

		public SortOrder AmmoSortOrder
			{
			get
				{
				return (m_AmmoSortOrder);
				}
			set
				{
				m_AmmoSortOrder = value;
				}
			}

		//============================================================================*
		// AmmoTestSortColumn Property
		//============================================================================*

		public int AmmoTestSortColumn
			{
			get
				{
				return (m_nAmmoTestSortColumn);
				}
			set
				{
				m_nAmmoTestSortColumn = value;
				}
			}

		//============================================================================*
		// AmmoTestSortOrder Property
		//============================================================================*

		public SortOrder AmmoTestSortOrder
			{
			get
				{
				return (m_AmmoTestSortOrder);
				}
			set
				{
				m_AmmoTestSortOrder = value;
				}
			}

		//============================================================================*
		// AutoBackup Property
		//============================================================================*

		public bool AutoBackup
			{
			get
				{
				return (m_fAutoBackup);
				}
			set
				{
				m_fAutoBackup = value;
				}
			}

		//============================================================================*
		// AutoCheck Property
		//============================================================================*

		public bool AutoCheck
			{
			get
				{
				return (m_fAutoCheck);
				}
			set
				{
				m_fAutoCheck = value;
				}
			}

		//============================================================================*
		// AutoCheckNonZero Property
		//============================================================================*

		public bool AutoCheckNonZero
			{
			get
				{
				return (m_fAutoCheckNonZero);
				}
			set
				{
				m_fAutoCheckNonZero = value;
				}
			}

		//============================================================================*
		// AutoSaveTime Property
		//============================================================================*

		public int AutoSaveTime
			{
			get
				{
				return (m_nAutoSaveTime);
				}
			set
				{
				m_nAutoSaveTime = value;
				}
			}

		//============================================================================*
		// AverageCosts Property
		//============================================================================*

		public bool AverageCosts
			{
			get
				{
				return (!m_fUseLastPurchase);
				}
			set
				{
				m_fUseLastPurchase = !value;
				}
			}

		//============================================================================*
		// BackupFolder Property
		//============================================================================*

		public string BackupFolder
			{
			get
				{
				return (m_strBackupFolder);
				}
			set
				{
				m_strBackupFolder = value;
				}
			}

		//============================================================================*
		// BackupKeepDays Property
		//============================================================================*

		public int BackupKeepDays
			{
			get
				{
				return (m_nBackupKeepDays);
				}
			set
				{
				m_nBackupKeepDays = value;
				}
			}

		//============================================================================*
		// BackupOK Property
		//============================================================================*

		public bool BackupOK
			{
			get
				{
				return (m_fBackupOK);
				}
			set
				{
				m_fBackupOK = value;
				}
			}

		//============================================================================*
		// BallisticsBatch Property
		//============================================================================*

		public cBatch BallisticsBatch
			{
			get
				{
				return (m_BallisticsBatch);
				}
			set
				{
				m_BallisticsBatch = value;
				}
			}

		//============================================================================*
		// BallisticsBullet Property
		//============================================================================*

		public cBullet BallisticsBullet
			{
			get
				{
				return (m_BallisticsBullet);
				}
			set
				{
				m_BallisticsBullet = value;
				}
			}

		//============================================================================*
		// BallisticsCaliber Property
		//============================================================================*

		public cCaliber BallisticsCaliber
			{
			get
				{
				return (m_BallisticsCaliber);
				}
			set
				{
				m_BallisticsCaliber = value;
				}
			}

		//============================================================================*
		// BallisticsCharge Property
		//============================================================================*

		public double BallisticsCharge
			{
			get
				{
				return (m_BallisticsCharge);
				}
			set
				{
				m_BallisticsCharge = value;
				}
			}

		//============================================================================*
		// BallisticsFirearm Property
		//============================================================================*

		public cFirearm BallisticsFirearm
			{
			get
				{
				return (m_BallisticsFirearm);
				}
			set
				{
				m_BallisticsFirearm = value;
				}
			}

		//============================================================================*
		// BallisticsFirearmType Property
		//============================================================================*

		public cFirearm.eFireArmType BallisticsFirearmType
			{
			get
				{
				return (m_BallisticsFirearmType);
				}
			set
				{
				m_BallisticsFirearmType = value;
				}
			}

		//============================================================================*
		// BallisticsData Property
		//============================================================================*

		public cBallistics BallisticsData
			{
			get
				{
				return (m_BallisticsData);
				}
			set
				{
				m_BallisticsData = value;
				}
			}

		//============================================================================*
		// BallisticsLoad Property
		//============================================================================*

		public cLoad BallisticsLoad
			{
			get
				{
				return (m_BallisticsLoad);
				}
			set
				{
				m_BallisticsLoad = value;
				}
			}

		//============================================================================*
		// BallisticsPreviewLocation Property
		//============================================================================*

		public Point BallisticsPreviewLocation
			{
			get
				{
				return (m_BallisticsPreviewLocation);
				}
			set
				{
				m_BallisticsPreviewLocation = value;
				}
			}

		//============================================================================*
		// BallisticsPreviewMaximized Property
		//============================================================================*

		public bool BallisticsPreviewMaximized
			{
			get
				{
				return (m_fBallisticsPreviewMaximized);
				}
			set
				{
				m_fBallisticsPreviewMaximized = value;
				}
			}

		//============================================================================*
		// BallisticsPreviewSize Property
		//============================================================================*

		public Size BallisticsPreviewSize
			{
			get
				{
				return (m_BallisticsPreviewSize);
				}
			set
				{
				m_BallisticsPreviewSize = value;
				}
			}

		//============================================================================*
		// BallisticsUseSF Property
		//============================================================================*

		public bool BallisticsUseSF
			{
			get
				{
				return (m_fBallisticsUseSF);
				}
			set
				{
				m_fBallisticsUseSF = value;
				}
			}

		//============================================================================*
		// BatchEditorBullet Property
		//============================================================================*

		public cBullet BatchEditorBullet
			{
			get
				{
				return (m_BatchEditorBullet);
				}
			set
				{
				m_BatchEditorBullet = value;
				}
			}

		//============================================================================*
		// BatchEditorCaliber Property
		//============================================================================*

		public cCaliber BatchEditorCaliber
			{
			get
				{
				return (m_BatchEditorCaliber);
				}
			set
				{
				m_BatchEditorCaliber = value;
				}
			}

		//============================================================================*
		// BatchEditorFirearmType Property
		//============================================================================*

		public cFirearm.eFireArmType BatchEditorFirearmType
			{
			get
				{
				return (m_BatchEditorFirearmType);
				}
			set
				{
				m_BatchEditorFirearmType = value;
				}
			}

		//============================================================================*
		// BatchEditorIgnoreInventory Property
		//============================================================================*

		public bool BatchEditorIgnoreInventory
			{
			get
				{
				return (m_fBatchEditorIgnoreInventory);
				}
			set
				{
				m_fBatchEditorIgnoreInventory = value;
				}
			}

		//============================================================================*
		// BatchEditorPowder Property
		//============================================================================*

		public cPowder BatchEditorPowder
			{
			get
				{
				return (m_BatchEditorPowder);
				}
			set
				{
				m_BatchEditorPowder = value;
				}
			}

		//============================================================================*
		// BatchLoadSortColumn Property
		//============================================================================*

		public int BatchLoadSortColumn
			{
			get
				{
				return (m_nBatchLoadSortColumn);
				}
			set
				{
				m_nBatchLoadSortColumn = value;
				}
			}

		//============================================================================*
		// BatchLoadSortOrder Property
		//============================================================================*

		public SortOrder BatchLoadSortOrder
			{
			get
				{
				return (m_BatchLoadSortOrder);
				}
			set
				{
				m_BatchLoadSortOrder = value;
				}
			}

		//============================================================================*
		// BatchPrintPaper Property
		//============================================================================*

		public int BatchPrintPaper
			{
			get
				{
				return (m_nBatchPrintPaper);
				}
			set
				{
				m_nBatchPrintPaper = value;
				}
			}

		//============================================================================*
		// BatchPrintStartLabel Property
		//============================================================================*

		public int BatchPrintStartLabel
			{
			get
				{
				return (m_nBatchPrintStartLabel);
				}
			set
				{
				m_nBatchPrintStartLabel = value;
				}
			}

		//============================================================================*
		// BatchSortColumn Property
		//============================================================================*

		public int BatchSortColumn
			{
			get
				{
				return (m_nBatchSortColumn);
				}
			set
				{
				m_nBatchSortColumn = value;
				}
			}

		//============================================================================*
		// BatchSortOrder Property
		//============================================================================*

		public SortOrder BatchSortOrder
			{
			get
				{
				return (m_BatchSortOrder);
				}
			set
				{
				m_BatchSortOrder = value;
				}
			}

		//============================================================================*
		// BulletCaliberSortColumn Property
		//============================================================================*

		public int BulletCaliberSortColumn
			{
			get
				{
				return (m_nBulletCaliberSortColumn);
				}
			set
				{
				m_nBulletCaliberSortColumn = value;
				}
			}

		//============================================================================*
		// BulletCaliberSortOrder Property
		//============================================================================*

		public SortOrder BulletCaliberSortOrder
			{
			get
				{
				return (m_BulletCaliberSortOrder);
				}
			set
				{
				m_BulletCaliberSortOrder = value;
				}
			}

		//============================================================================*
		// BulletSortColumn Property
		//============================================================================*

		public int BulletSortColumn
			{
			get
				{
				return (m_nBulletSortColumn);
				}
			set
				{
				m_nBulletSortColumn = value;
				}
			}

		//============================================================================*
		// BulletSortOrder Property
		//============================================================================*

		public SortOrder BulletSortOrder
			{
			get
				{
				return (m_BulletSortOrder);
				}
			set
				{
				m_BulletSortOrder = value;
				}
			}

		//============================================================================*
		// BulletWeightDecimals Property
		//============================================================================*

		public int BulletWeightDecimals
			{
			get
				{
				return (m_nBulletWeightDecimals);
				}
			set
				{
				m_nBulletWeightDecimals = value;
				}
			}

		//============================================================================*
		// CaliberSortColumn Property
		//============================================================================*

		public int CaliberSortColumn
			{
			get
				{
				return (m_nCaliberSortColumn);
				}
			set
				{
				m_nCaliberSortColumn = value;
				}
			}

		//============================================================================*
		// CaliberSortOrder Property
		//============================================================================*

		public SortOrder CaliberSortOrder
			{
			get
				{
				return (m_CaliberSortOrder);
				}
			set
				{
				m_CaliberSortOrder = value;
				}
			}

		//============================================================================*
		// CalibrationBackcolor Property
		//============================================================================*

		public Color CalibrationBackcolor
			{
			get
				{
				return (m_CalibrationBackcolor);
				}
			set
				{
				m_CalibrationBackcolor = value;
				}
			}

		//============================================================================*
		// CalibrationForecolor Property
		//============================================================================*

		public Color CalibrationForecolor
			{
			get
				{
				return (m_CalibrationForecolor);
				}
			set
				{
				m_CalibrationForecolor = value;
				}
			}

		//============================================================================*
		// CanWeightDecimals Property
		//============================================================================*

		public int CanWeightDecimals
			{
			get
				{
				return (m_nCanWeightDecimals);
				}
			set
				{
				m_nCanWeightDecimals = value;
				}
			}

		//============================================================================*
		// CaseSortColumn Property
		//============================================================================*

		public int CaseSortColumn
			{
			get
				{
				return (m_nCaseSortColumn);
				}
			set
				{
				m_nCaseSortColumn = value;
				}
			}

		//============================================================================*
		// CaseSortOrder Property
		//============================================================================*

		public SortOrder CaseSortOrder
			{
			get
				{
				return (m_CaseSortOrder);
				}
			set
				{
				m_CaseSortOrder = value;
				}
			}

		//============================================================================*
		// ChargeSortColumn Property
		//============================================================================*

		public int ChargeSortColumn
			{
			get
				{
				return (m_nChargeSortColumn);
				}
			set
				{
				m_nChargeSortColumn = value;
				}
			}

		//============================================================================*
		// ChargeSortOrder Property
		//============================================================================*

		public SortOrder ChargeSortOrder
			{
			get
				{
				return (m_ChargeSortOrder);
				}
			set
				{
				m_ChargeSortOrder = value;
				}
			}

		//============================================================================*
		// ChargeTestSortColumn Property
		//============================================================================*

		public int ChargeTestSortColumn
			{
			get
				{
				return (m_nChargeTestSortColumn);
				}
			set
				{
				m_nChargeTestSortColumn = value;
				}
			}

		//============================================================================*
		// ChargeTestSortOrder Property
		//============================================================================*

		public SortOrder ChargeTestSortOrder
			{
			get
				{
				return (m_ChargeTestSortOrder);
				}
			set
				{
				m_ChargeTestSortOrder = value;
				}
			}

		//============================================================================*
		// ConversionDecimals Property
		//============================================================================*

		public int ConversionDecimals
			{
			get
				{
				return (m_nConversionDecimals);
				}
			set
				{
				m_nConversionDecimals = value;
				}
			}

		//============================================================================*
		// CopyChargeSortColumn Property
		//============================================================================*

		public int CopyChargeSortColumn
			{
			get
				{
				return (m_nCopyChargeSortColumn);
				}
			set
				{
				m_nCopyChargeSortColumn = value;
				}
			}

		//============================================================================*
		// CopyChargeSortOrder Property
		//============================================================================*

		public SortOrder CopyChargeSortOrder
			{
			get
				{
				return (m_CopyChargeSortOrder);
				}
			set
				{
				m_CopyChargeSortOrder = value;
				}
			}

		//============================================================================*
		// CostAnalysisPreviewLocation Property
		//============================================================================*

		public Point CostAnalysisPreviewLocation
			{
			get
				{
				return (m_CostAnalysisPreviewLocation);
				}
			set
				{
				m_CostAnalysisPreviewLocation = value;
				}
			}

		//============================================================================*
		// CostAnalysisPreviewMaximized Property
		//============================================================================*

		public bool CostAnalysisPreviewMaximized
			{
			get
				{
				return (m_fCostAnalysisPreviewMaximized);
				}
			set
				{
				m_fCostAnalysisPreviewMaximized = value;
				}
			}

		//============================================================================*
		// CostAnalysisPreviewSize Property
		//============================================================================*

		public Size CostAnalysisPreviewSize
			{
			get
				{
				return (m_CostAnalysisPreviewSize);
				}
			set
				{
				m_CostAnalysisPreviewSize = value;
				}
			}

		//============================================================================*
		// Currency Property
		//============================================================================*

		public string Currency
			{
			get
				{
				return (m_strCurrency);
				}
			set
				{
				m_strCurrency = value;
				}
			}

		//============================================================================*
		// Dev Property
		//============================================================================*

		public bool Dev
			{
			get
				{
				return (m_fDev);
				}
			set
				{
				m_fDev = value;
				}
			}

		//============================================================================*
		// DimensionDecimals Property
		//============================================================================*

		public int DimensionDecimals
			{
			get
				{
				return (m_nDimensionDecimals);
				}
			set
				{
				m_nDimensionDecimals = value;
				}
			}

		//============================================================================*
		// DateFormat Property
		//============================================================================*

		public int DateFormat
			{
			get
				{
				return (m_nDateFormat);
				}
			set
				{
				m_nDateFormat = value;
				}
			}

		//============================================================================*
		// DateFormatString Property
		//============================================================================*

		public string DateFormatString
			{
			get
				{
				return (m_strDateFormat);
				}
			set
				{
				m_strDateFormat = value;
				}
			}

		//============================================================================*
		// EvaluationListLocation Property
		//============================================================================*

		public Point EvaluationListLocation
			{
			get
				{
				return (m_EvaluationListLocation);
				}
			set
				{
				m_EvaluationListLocation = value;
				}
			}

		//============================================================================*
		// EvaluationListSize Property
		//============================================================================*

		public Size EvaluationListSize
			{
			get
				{
				return (m_EvaluationListSize);
				}
			set
				{
				m_EvaluationListSize = value;
				}
			}

		//============================================================================*
		// EvaluationSortColumn Property
		//============================================================================*

		public int EvaluationSortColumn
			{
			get
				{
				return (m_nEvaluationSortColumn);
				}
			set
				{
				m_nEvaluationSortColumn = value;
				}
			}

		//============================================================================*
		// EvaluationSortOrder Property
		//============================================================================*

		public SortOrder EvaluationSortOrder
			{
			get
				{
				return (m_EvaluationSortOrder);
				}
			set
				{
				m_EvaluationSortOrder = value;
				}
			}

		//============================================================================*
		// ExtremesColor Property
		//============================================================================*

		public Color ExtremesColor
			{
			get
				{
				return (m_ExtremesColor);
				}
			set
				{
				m_ExtremesColor = value;
				}
			}

		//============================================================================*
		// FirearmBulletSortColumn Property
		//============================================================================*

		public int FirearmBulletSortColumn
			{
			get
				{
				return (m_nFirearmBulletSortColumn);
				}
			set
				{
				m_nFirearmBulletSortColumn = value;
				}
			}

		//============================================================================*
		// FirearmBulletSortOrder Property
		//============================================================================*

		public SortOrder FirearmBulletSortOrder
			{
			get
				{
				return (m_FirearmBulletSortOrder);
				}
			set
				{
				m_FirearmBulletSortOrder = value;
				}
			}

		//============================================================================*
		// FirearmListPreviewLocation Property
		//============================================================================*

		public Point FirearmListPreviewLocation
			{
			get
				{
				return (m_FirearmListPreviewLocation);
				}
			set
				{
				m_FirearmListPreviewLocation = value;
				}
			}

		//============================================================================*
		// FirearmListPreviewMaximized Property
		//============================================================================*

		public bool FirearmListPreviewMaximized
			{
			get
				{
				return (m_fFirearmListPreviewMaximized);
				}
			set
				{
				m_fFirearmListPreviewMaximized = value;
				}
			}

		//============================================================================*
		// FirearmListPreviewSize Property
		//============================================================================*

		public Size FirearmListPreviewSize
			{
			get
				{
				return (m_FirearmListPreviewSize);
				}
			set
				{
				m_FirearmListPreviewSize = value;
				}
			}

		//============================================================================*
		// FirearmPrintAll Property
		//============================================================================*

		public bool FirearmPrintAll
			{
			get
				{
				return (m_fFirearmPrintAll);
				}
			set
				{
				m_fFirearmPrintAll = value;
				}
			}

		//============================================================================*
		// FirearmPrintDetail Property
		//============================================================================*

		public bool FirearmPrintDetail
			{
			get
				{
				return (m_fFirearmPrintDetail);
				}
			set
				{
				m_fFirearmPrintDetail = value;
				}
			}

		//============================================================================*
		// FirearmPrintSpecs Property
		//============================================================================*

		public bool FirearmPrintSpecs
			{
			get
				{
				return (m_fFirearmPrintSpecs);
				}
			set
				{
				m_fFirearmPrintSpecs = value;
				}
			}

		//============================================================================*
		// FirearmSortColumn Property
		//============================================================================*

		public int FirearmSortColumn
			{
			get
				{
				return (m_nFirearmSortColumn);
				}
			set
				{
				m_nFirearmSortColumn = value;
				}
			}

		//============================================================================*
		// FirearmSortOrder Property
		//============================================================================*

		public SortOrder FirearmSortOrder
			{
			get
				{
				return (m_FirearmSortOrder);
				}
			set
				{
				m_FirearmSortOrder = value;
				}
			}

		//============================================================================*
		// GetColumn()
		//============================================================================*

		public cColumn GetColumn(eApplicationListView eListView, string strName)
			{
			foreach (cColumn CheckColumn in m_ColumnList)
				{
				if (CheckColumn.ListView == eListView &&
					CheckColumn.Name == strName)
					return (CheckColumn);
				}

			return (null);
			}

		//============================================================================*
		// GetColumnIndex()
		//============================================================================*

		public int GetColumnIndex(eApplicationListView eListView, string strName)
			{
			foreach (cColumn CheckColumn in m_ColumnList)
				{
				if (CheckColumn.ListView == eListView && CheckColumn.Name == strName)
					return (CheckColumn.DisplayIndex);
				}

			return (-1);
			}

		//============================================================================*
		// GetColumnWidth()
		//============================================================================*

		public int GetColumnWidth(eApplicationListView eListView, string strName)
			{
			foreach (cColumn CheckColumn in m_ColumnList)
				{
				if (CheckColumn.ListView == eListView &&
					CheckColumn.Name == strName)
					return (CheckColumn.Width);
				}

			return (0);
			}

		//============================================================================*
		// GroupDecimals Property
		//============================================================================*

		public int GroupDecimals
			{
			get
				{
				return (m_nGroupDecimals);
				}
			set
				{
				m_nGroupDecimals = value;
				}
			}

		//============================================================================*
		// HideUncheckedCalibers Property
		//============================================================================*

		public bool HideUncheckedCalibers
			{
			get
				{
				return (m_fHideUncheckedCalibers);
				}
			set
				{
				m_fHideUncheckedCalibers = value;
				}
			}

		//============================================================================*
		// HideUncheckedSupplies Property
		//============================================================================*

		public bool HideUncheckedSupplies
			{
			get
				{
				return (m_fHideUncheckedSupplies);
				}
			set
				{
				m_fHideUncheckedSupplies = value;
				}
			}

		//============================================================================*
		// IncludeTaxShipping Property
		//============================================================================*

		public bool IncludeTaxShipping
			{
			get
				{
				return (m_fIncludeTaxShipping);
				}
			set
				{
				m_fIncludeTaxShipping = value;
				}
			}

		//============================================================================*
		// IndentMultiplier Property
		//============================================================================*

		public int IndentMultiplier
			{
			get
				{
				return (m_nIndentMultiplier);
				}
			set
				{
				m_nIndentMultiplier = value;
				}
			}

		//============================================================================*
		// LastActivity Property
		//============================================================================*

		public cTransaction.eTransactionType LastActivity
			{
			get
				{
				return (m_eLastActivityType);
				}
			set
				{
				m_eLastActivityType = value;
				}
			}

		//============================================================================*
		// LastAddStockReason Property
		//============================================================================*

		public string LastAddStockReason
			{
			get
				{
				return (m_strLastAddStockReason);
				}
			set
				{
				m_strLastAddStockReason = value;
				}
			}

		//============================================================================*
		// LastAmmo Property
		//============================================================================*

		public cAmmo LastAmmo
			{
			get
				{
				return (m_LastAmmo);
				}
			set
				{
				m_LastAmmo = value;
				}
			}

		//============================================================================*
		// LastAmmoCaliber Property
		//============================================================================*

		public cCaliber LastAmmoCaliber
			{
			get
				{
				return (m_LastAmmoCaliber);
				}
			set
				{
				m_LastAmmoCaliber = value;
				}
			}

		//============================================================================*
		// LastAmmoManufacturer Property
		//============================================================================*

		public cManufacturer LastAmmoManufacturer
			{
			get
				{
				return (m_LastAmmoManufacturer);
				}
			set
				{
				m_LastAmmoManufacturer = value;
				}
			}

		//============================================================================*
		// LastAmmoSelected Property
		//============================================================================*

		public cAmmo LastAmmoSelected
			{
			get
				{
				return (m_LastAmmoSelected);
				}
			set
				{
				m_LastAmmoSelected = value;
				}
			}

		//============================================================================*
		// LastBatch Property
		//============================================================================*

		public cBatch LastBatch
			{
			get
				{
				return (m_LastBatch);
				}
			set
				{
				m_LastBatch = value;
				}
			}

		//============================================================================*
		// LastBatchBulletSelected Property
		//============================================================================*

		public cBullet LastBatchBulletSelected
			{
			get
				{
				return (m_LastBatchBulletSelected);
				}
			set
				{
				m_LastBatchBulletSelected = value;
				}
			}

		//============================================================================*
		// LastBatchCaliberSelected Property
		//============================================================================*

		public cCaliber LastBatchCaliberSelected
			{
			get
				{
				return (m_LastBatchCaliberSelected);
				}
			set
				{
				m_LastBatchCaliberSelected = value;
				}
			}

		//============================================================================*
		// LastBatchLoadBulletSelected Property
		//============================================================================*

		public cBullet LastBatchLoadBulletSelected
			{
			get
				{
				return (m_LastBatchLoadBulletSelected);
				}
			set
				{
				m_LastBatchLoadBulletSelected = value;
				}
			}

		//============================================================================*
		// LastBatchLoadCaliberSelected Property
		//============================================================================*

		public cCaliber LastBatchLoadCaliberSelected
			{
			get
				{
				return (m_LastBatchLoadCaliberSelected);
				}
			set
				{
				m_LastBatchLoadCaliberSelected = value;
				}
			}

		//============================================================================*
		// LastBatchLoadPowderSelected Property
		//============================================================================*

		public cPowder LastBatchLoadPowderSelected
			{
			get
				{
				return (m_LastBatchLoadPowderSelected);
				}
			set
				{
				m_LastBatchLoadPowderSelected = value;
				}
			}

		//============================================================================*
		// LastBatchLoadSelected Property
		//============================================================================*

		public cLoad LastBatchLoadSelected
			{
			get
				{
				return (m_LastBatchLoadSelected);
				}
			set
				{
				m_LastBatchLoadSelected = value;
				}
			}

		//============================================================================*
		// LastBatchPowderSelected Property
		//============================================================================*

		public cPowder LastBatchPowderSelected
			{
			get
				{
				return (m_LastBatchPowderSelected);
				}
			set
				{
				m_LastBatchPowderSelected = value;
				}
			}

		//============================================================================*
		// LastBatchTest Property
		//============================================================================*

		public cBatchTest LastBatchTest
			{
			get
				{
				return (m_LastBatchTest);
				}
			set
				{
				m_LastBatchTest = value;
				}
			}

		//============================================================================*
		// LastBatchSelected Property
		//============================================================================*

		public cBatch LastBatchSelected
			{
			get
				{
				return (m_LastBatchSelected);
				}
			set
				{
				m_LastBatchSelected = value;
				}
			}

		//============================================================================*
		// LastBatchTestSelected Property
		//============================================================================*

		public cBatchTest LastBatchTestSelected
			{
			get
				{
				return (m_LastBatchTestSelected);
				}
			set
				{
				m_LastBatchTestSelected = value;
				}
			}

		//============================================================================*
		// LastBullet Property
		//============================================================================*

		public cBullet LastBullet
			{
			get
				{
				return (m_LastBullet);
				}
			set
				{
				m_LastBullet = value;
				}
			}

		//============================================================================*
		// LastBulletCaliber Property
		//============================================================================*

		public cCaliber LastBulletCaliber
			{
			get
				{
				return (m_LastBulletCaliber);
				}
			set
				{
				m_LastBulletCaliber = value;
				}
			}

		//============================================================================*
		// LastBulletCaliberCOL Property
		//============================================================================*

		public double LastBulletCaliberCOL
			{
			get
				{
				return (m_dLastBulletCaliberCOL);
				}
			set
				{
				m_dLastBulletCaliberCOL = value;
				}
			}

		//============================================================================*
		// LastBulletCaliberSelected Property
		//============================================================================*

		public cBulletCaliber LastBulletCaliberSelected
			{
			get
				{
				return (m_LastBulletCaliberSelected);
				}
			set
				{
				m_LastBulletCaliberSelected = value;
				}
			}

		//============================================================================*
		// LastBulletSelected Property
		//============================================================================*

		public cBullet LastBulletSelected
			{
			get
				{
				return (m_LastBulletSelected);
				}
			set
				{
				m_LastBulletSelected = value;
				}
			}

		//============================================================================*
		// LastCaliber Property
		//============================================================================*

		public cCaliber LastCaliber
			{
			get
				{
				return (m_LastCaliber);
				}
			set
				{
				m_LastCaliber = value;
				}
			}

		//============================================================================*
		// LastCaliberSelected Property
		//============================================================================*

		public cCaliber LastCaliberSelected
			{
			get
				{
				return (m_LastCaliberSelected);
				}
			set
				{
				m_LastCaliberSelected = value;
				}
			}

		//============================================================================*
		// LastCase Property
		//============================================================================*

		public cCase LastCase
			{
			get
				{
				return (m_LastCase);
				}
			set
				{
				m_LastCase = value;
				}
			}

		//============================================================================*
		// LastCaseSelected Property
		//============================================================================*

		public cCase LastCaseSelected
			{
			get
				{
				return (m_LastCaseSelected);
				}
			set
				{
				m_LastCaseSelected = value;
				}
			}

		//============================================================================*
		// LastCharge Property
		//============================================================================*

		public cCharge LastCharge
			{
			get
				{
				return (m_LastCharge);
				}
			set
				{
				m_LastCharge = value;
				}
			}

		//============================================================================*
		// LastChargeTest Property
		//============================================================================*

		public cChargeTest LastChargeTest
			{
			get
				{
				return (m_LastChargeTest);
				}
			set
				{
				m_LastChargeTest = value;
				}
			}

		//============================================================================*
		// LastChargeSelected Property
		//============================================================================*

		public cCharge LastChargeSelected
			{
			get
				{
				return (m_LastChargeSelected);
				}
			set
				{
				m_LastChargeSelected = value;
				}
			}

		//============================================================================*
		// LastCopyLoadSelected Property
		//============================================================================*

		public cLoad LastCopyLoadSelected
			{
			get
				{
				return (m_LastCopyLoadSelected);
				}
			set
				{
				m_LastCopyLoadSelected = value;
				}
			}

		//============================================================================*
		// LastFiredLocation Property
		//============================================================================*

		public string LastFiredLocation
			{
			get
				{
				return (m_strLastFiredLocation);
				}
			set
				{
				m_strLastFiredLocation = value;
				}
			}

		//============================================================================*
		// LastFirearm Property
		//============================================================================*

		public cFirearm LastFirearm
			{
			get
				{
				return (m_LastFirearm);
				}
			set
				{
				m_LastFirearm = value;
				}
			}

		//============================================================================*
		// LastFirearmBullet Property
		//============================================================================*

		public cFirearmBullet LastFirearmBullet
			{
			get
				{
				return (m_LastFirearmBullet);
				}
			set
				{
				m_LastFirearmBullet = value;
				}
			}

		//============================================================================*
		// LastFirearmBulletSelected Property
		//============================================================================*

		public cFirearmBullet LastFirearmBulletSelected
			{
			get
				{
				return (m_LastFirearmBulletSelected);
				}
			set
				{
				m_LastFirearmBulletSelected = value;
				}
			}

		//============================================================================*
		// LastFirearmSelected Property
		//============================================================================*

		public cFirearm LastFirearmSelected
			{
			get
				{
				return (m_LastFirearmSelected);
				}
			set
				{
				m_LastFirearmSelected = value;
				}
			}

		//============================================================================*
		// LastLoad Property
		//============================================================================*

		public cLoad LastLoad
			{
			get
				{
				return (m_LastLoad);
				}
			set
				{
				m_LastLoad = value;
				}
			}

		//============================================================================*
		// LastLoadDataBulletSelected Property
		//============================================================================*

		public cBullet LastLoadDataBulletSelected
			{
			get
				{
				return (m_LastLoadDataBulletSelected);
				}
			set
				{
				m_LastLoadDataBulletSelected = value;
				}
			}

		//============================================================================*
		// LastLoadDataCaliberSelected Property
		//============================================================================*

		public cCaliber LastLoadDataCaliberSelected
			{
			get
				{
				return (m_LastLoadDataCaliberSelected);
				}
			set
				{
				m_LastLoadDataCaliberSelected = value;
				}
			}

		//============================================================================*
		// LastLoadDataPowderSelected Property
		//============================================================================*

		public cPowder LastLoadDataPowderSelected
			{
			get
				{
				return (m_LastLoadDataPowderSelected);
				}
			set
				{
				m_LastLoadDataPowderSelected = value;
				}
			}

		//============================================================================*
		// LastLoadSelected Property
		//============================================================================*

		public cLoad LastLoadSelected
			{
			get
				{
				return (m_LastLoadSelected);
				}
			set
				{
				m_LastLoadSelected = value;
				}
			}

		//============================================================================*
		// LastMainTabSelected Property
		//============================================================================*

		public string LastMainTabSelected
			{
			get
				{
				return (m_strLastMainTabSelected);
				}
			set
				{
				m_strLastMainTabSelected = value;
				}
			}

		//============================================================================*
		// LastManufacturerSelected Property
		//============================================================================*

		public cManufacturer LastManufacturerSelected
			{
			get
				{
				return (m_LastManufacturerSelected);
				}
			set
				{
				m_LastManufacturerSelected = value;
				}
			}

		//============================================================================*
		// LastPowder Property
		//============================================================================*

		public cPowder LastPowder
			{
			get
				{
				return (m_LastPowder);
				}
			set
				{
				m_LastPowder = value;
				}
			}

		//============================================================================*
		// LastPowderSelected Property
		//============================================================================*

		public cPowder LastPowderSelected
			{
			get
				{
				return (m_LastPowderSelected);
				}
			set
				{
				m_LastPowderSelected = value;
				}
			}

		//============================================================================*
		// LastPrimer Property
		//============================================================================*

		public cPrimer LastPrimer
			{
			get
				{
				return (m_LastPrimer);
				}
			set
				{
				m_LastPrimer = value;
				}
			}

		//============================================================================*
		// LastPrimerSelected Property
		//============================================================================*

		public cPrimer LastPrimerSelected
			{
			get
				{
				return (m_LastPrimerSelected);
				}
			set
				{
				m_LastPrimerSelected = value;
				}
			}

		//============================================================================*
		// LastPurchaseSource Property
		//============================================================================*

		public string LastPurchaseSource
			{
			get
				{
				return (m_strLastPurchaseSource);
				}
			set
				{
				m_strLastPurchaseSource = value;
				}
			}

		//============================================================================*
		// LastReduceStockReason Property
		//============================================================================*

		public string LastReduceStockReason
			{
			get
				{
				return (m_strLastReduceStockReason);
				}
			set
				{
				m_strLastReduceStockReason = value;
				}
			}

		//============================================================================*
		// LastSupplyTypeSelected Property
		//============================================================================*

		public cSupply.eSupplyTypes LastSupplyTypeSelected
			{
			get
				{
				return (m_eLastSupplyTypeSelected);
				}
			set
				{
				m_eLastSupplyTypeSelected = value;
				}
			}

		//============================================================================*
		// LastTransaction Property
		//============================================================================*

		public cTransaction LastTransaction
			{
			get
				{
				return (m_LastTransaction);
				}
			set
				{
				m_LastTransaction = value;
				}
			}

		//============================================================================*
		// LastTransactionSelected Property
		//============================================================================*

		public cTransaction LastTransactionSelected
			{
			get
				{
				return (m_LastTransactionSelected);
				}
			set
				{
				m_LastTransactionSelected = value;
				}
			}

		//============================================================================*
		// FirearmDecimals Property
		//============================================================================*

		public int FirearmDecimals
			{
			get
				{
				return (m_nFirearmDecimals);
				}
			set
				{
				m_nFirearmDecimals = value;
				}
			}

		//============================================================================*
		// LoadDataFirearmType Property
		//============================================================================*

		public cFirearm.eFireArmType LoadDataFirearmType
			{
			get
				{
				return (m_LoadDataFirearmType);
				}
			set
				{
				m_LoadDataFirearmType = value;
				}
			}

		//============================================================================*
		// LoadDataSortColumn Property
		//============================================================================*

		public int LoadDataSortColumn
			{
			get
				{
				return (m_nLoadDataSortColumn);
				}
			set
				{
				m_nLoadDataSortColumn = value;
				}
			}

		//============================================================================*
		// LoadSortOrder Property
		//============================================================================*

		public SortOrder LoadDataSortOrder
			{
			get
				{
				return (m_LoadDataSortOrder);
				}
			set
				{
				m_LoadDataSortOrder = value;
				}
			}

		//============================================================================*
		// MainFormLocation Property
		//============================================================================*

		public Point MainFormLocation
			{
			get
				{
				return (m_MainFormLocation);
				}
			set
				{
				m_MainFormLocation = value;
				}
			}

		//============================================================================*
		// MainFormSize Property
		//============================================================================*

		public Size MainFormSize
			{
			get
				{
				return (m_MainFormSize);
				}
			set
				{
				m_MainFormSize = value;
				}
			}

		//============================================================================*
		// ManufacturerSortColumn Property
		//============================================================================*

		public int ManufacturerSortColumn
			{
			get
				{
				return (m_nManufacturerSortColumn);
				}
			set
				{
				m_nManufacturerSortColumn = value;
				}
			}

		//============================================================================*
		// ManufacturerSortOrder Property
		//============================================================================*

		public SortOrder ManufacturerSortOrder
			{
			get
				{
				return (m_ManufacturerSortOrder);
				}
			set
				{
				m_ManufacturerSortOrder = value;
				}
			}

		//============================================================================*
		// Maximized Property
		//============================================================================*

		public bool Maximized
			{
			get
				{
				return (m_fMaximized);
				}
			set
				{
				m_fMaximized = value;
				}
			}

		//============================================================================*
		// MetricAltitudes Property
		//============================================================================*

		public bool MetricAltitudes
			{
			get
				{
				return (m_fMetricAltitudes);
				}
			set
				{
				m_fMetricAltitudes = value;
				}
			}

		//============================================================================*
		// MetricBulletWeights Property
		//============================================================================*

		public bool MetricBulletWeights
			{
			get
				{
				return (m_fMetricBulletWeights);
				}
			set
				{
				m_fMetricBulletWeights = value;
				}
			}

		//============================================================================*
		// MetricCanWeights Property
		//============================================================================*

		public bool MetricCanWeights
			{
			get
				{
				return (m_fMetricCanWeights);
				}
			set
				{
				m_fMetricCanWeights = value;
				}
			}

		//============================================================================*
		// MetricDimensions Property
		//============================================================================*

		public bool MetricDimensions
			{
			get
				{
				return (m_fMetricDimensions);
				}
			set
				{
				m_fMetricDimensions = value;
				}
			}

		//============================================================================*
		// MetricFirearms Property
		//============================================================================*

		public bool MetricFirearms
			{
			get
				{
				return (m_fMetricFirearms);
				}
			set
				{
				m_fMetricFirearms = value;
				}
			}

		//============================================================================*
		// MetricGroups Property
		//============================================================================*

		public bool MetricGroups
			{
			get
				{
				return (m_fMetricGroups);
				}
			set
				{
				m_fMetricGroups = value;
				}
			}

		//============================================================================*
		// MetricPowderWeights Property
		//============================================================================*

		public bool MetricPowderWeights
			{
			get
				{
				return (m_fMetricPowderWeights);
				}
			set
				{
				m_fMetricPowderWeights = value;
				}
			}

		//============================================================================*
		// MetricPressures Property
		//============================================================================*

		public bool MetricPressures
			{
			get
				{
				return (m_fMetricPressures);
				}
			set
				{
				m_fMetricPressures = value;
				}
			}

		//============================================================================*
		// MetricRanges Property
		//============================================================================*

		public bool MetricRanges
			{
			get
				{
				return (m_fMetricRanges);
				}
			set
				{
				m_fMetricRanges = value;
				}
			}

		//============================================================================*
		// MetricShotWeights Property
		//============================================================================*

		public bool MetricShotWeights
			{
			get
				{
				return (m_fMetricShotWeights);
				}
			set
				{
				m_fMetricShotWeights = value;
				}
			}

		//============================================================================*
		// MetricTemperatures Property
		//============================================================================*

		public bool MetricTemperatures
			{
			get
				{
				return (m_fMetricTemperatures);
				}
			set
				{
				m_fMetricTemperatures = value;
				}
			}

		//============================================================================*
		// MetricVelocities Property
		//============================================================================*

		public bool MetricVelocities
			{
			get
				{
				return (m_fMetricVelocities);
				}
			set
				{
				m_fMetricVelocities = value;
				}
			}

		//============================================================================*
		// NextBatchID Property
		//============================================================================*

		public int NextBatchID
			{
			get
				{
				return (m_nNextBatchID);
				}
			set
				{
				m_nNextBatchID = value;
				}
			}

		//============================================================================*
		// OffsetColor Property
		//============================================================================*

		public Color OffsetColor
			{
			get
				{
				return (m_OffsetColor);
				}
			set
				{
				m_OffsetColor = value;
				}
			}

		//============================================================================*
		// PowderSortColumn Property
		//============================================================================*

		public int PowderSortColumn
			{
			get
				{
				return (m_nPowderSortColumn);
				}
			set
				{
				m_nPowderSortColumn = value;
				}
			}

		//============================================================================*
		// PowderSortOrder Property
		//============================================================================*

		public SortOrder PowderSortOrder
			{
			get
				{
				return (m_PowderSortOrder);
				}
			set
				{
				m_PowderSortOrder = value;
				}
			}

		//============================================================================*
		// PowderWeightDecimals Property
		//============================================================================*

		public int PowderWeightDecimals
			{
			get
				{
				return (m_nPowderWeightDecimals);
				}
			set
				{
				m_nPowderWeightDecimals = value;
				}
			}

		//============================================================================*
		// PrimerSortColumn Property
		//============================================================================*

		public int PrimerSortColumn
			{
			get
				{
				return (m_nPrimerSortColumn);
				}
			set
				{
				m_nPrimerSortColumn = value;
				}
			}

		//============================================================================*
		// PrimerSortOrder Property
		//============================================================================*

		public SortOrder PrimerSortOrder
			{
			get
				{
				return (m_PrimerSortOrder);
				}
			set
				{
				m_PrimerSortOrder = value;
				}
			}

		//============================================================================*
		// ReticleColor Property
		//============================================================================*

		public Color ReticleColor
			{
			get
				{
				return (m_ReticleColor);
				}
			set
				{
				m_ReticleColor = value;
				}
			}

		//============================================================================*
		// SetColumnIndex()
		//============================================================================*

		public void SetColumnIndex(eApplicationListView eApplicationListView, string strHeaderName, int nDisplayIndex)
			{
			bool fFound = false;

			foreach (cColumn CheckColumn in m_ColumnList)
				{
				if (CheckColumn.ListView == eApplicationListView)
					{
					if (CheckColumn.Name == strHeaderName)
						{
						CheckColumn.DisplayIndex = nDisplayIndex;

						fFound = true;
						}
					else
						{
						if (CheckColumn.DisplayIndex == nDisplayIndex)
							CheckColumn.DisplayIndex++;
						}
					}
				}

			if (!fFound)
				{
				cColumn Column = new cColumn();
				Column.ListView = eApplicationListView;
				Column.Name = strHeaderName;
				Column.DisplayIndex = nDisplayIndex;

				m_ColumnList.Add(Column);
				}
			}

		//============================================================================*
		// SetColumnWidth()
		//============================================================================*

		public void SetColumnWidth(eApplicationListView eApplicationListView, string strHeaderName, int nWidth)
			{
			foreach (cColumn CheckColumn in m_ColumnList)
				{
				if (CheckColumn.ListView == eApplicationListView &&
					CheckColumn.Name == strHeaderName)
					{
					CheckColumn.Width = nWidth;

					return;
					}
				}

			cColumn Column = new cColumn();
			Column.ListView = eApplicationListView;
			Column.Name = strHeaderName;
			Column.Width = nWidth;

			m_ColumnList.Add(Column);
			}

		//============================================================================*
		// ShareFilePath Property
		//============================================================================*

		public string ShareFilePath
			{
			get
				{
				return (m_strShareFilePath);
				}
			set
				{
				m_strShareFilePath = value;
				}
			}

		//============================================================================*
		// ShoppingListPreviewLocation Property
		//============================================================================*

		public Point ShoppingListPreviewLocation
			{
			get
				{
				return (m_ShoppingListPreviewLocation);
				}
			set
				{
				m_ShoppingListPreviewLocation = value;
				}
			}

		//============================================================================*
		// ShoppingListPreviewMaximized Property
		//============================================================================*

		public bool ShoppingListPreviewMaximized
			{
			get
				{
				return (m_fShoppingListPreviewMaximized);
				}
			set
				{
				m_fShoppingListPreviewMaximized = value;
				}
			}

		//============================================================================*
		// ShoppingListPreviewSize Property
		//============================================================================*

		public Size ShoppingListPreviewSize
			{
			get
				{
				return (m_ShoppingListPreviewSize);
				}
			set
				{
				m_ShoppingListPreviewSize = value;
				}
			}

		//============================================================================*
		// ShotColor Property
		//============================================================================*

		public Color ShotColor
			{
			get
				{
				return (m_ShotColor);
				}
			set
				{
				m_ShotColor = value;
				}
			}

		//============================================================================*
		// ShotWeightDecimals Property
		//============================================================================*

		public int ShotWeightDecimals
			{
			get
				{
				return (m_nShotWeightDecimals);
				}
			set
				{
				m_nShotWeightDecimals = value;
				}
			}

		//============================================================================*
		// ShowApexMarker Property
		//============================================================================*

		public bool ShowApexMarker
			{
			get
				{
				return (m_fShowApexMarker);
				}
			set
				{
				m_fShowApexMarker = value;
				}
			}

		//============================================================================*
		// ShowArchivedBatches Property
		//============================================================================*

		public bool ShowArchivedBatches
			{
			get
				{
				return (m_fShowArchivedBatches);
				}
			set
				{
				m_fShowArchivedBatches = value;
				}
			}

		//============================================================================*
		// ShowArchivedTransactions Property
		//============================================================================*

		public bool ShowArchivedTransactions
			{
			get
				{
				return (m_fShowArchivedTransactions);
				}
			set
				{
				m_fShowArchivedTransactions = value;
				}
			}

		//============================================================================*
		// ShowDropChartRangeMarkers Property
		//============================================================================*

		public bool ShowDropChartRangeMarkers
			{
			get
				{
				return (m_fShowDropChartRangeMarkers);
				}
			set
				{
				m_fShowDropChartRangeMarkers = value;
				}
			}

		//============================================================================*
		// ShowBatchTransactions Property
		//============================================================================*

		public bool ShowBatchTransactions
			{
			get
				{
				return (m_fShowBatchTransactions);
				}
			set
				{
				m_fShowBatchTransactions = value;
				}
			}

		//============================================================================*
		// ShowGroundStrikeMarkers Property
		//============================================================================*

		public bool ShowGroundStrikeMarkers
			{
			get
				{
				return (m_fShowGroundStrikeMarkers);
				}
			set
				{
				m_fShowGroundStrikeMarkers = value;
				}
			}

		//============================================================================*
		// ShowTransonicMarkers Property
		//============================================================================*

		public bool ShowTransonicMarkers
			{
			get
				{
				return (m_fShowTransonicMarkers);
				}
			set
				{
				m_fShowTransonicMarkers = value;
				}
			}

		//============================================================================*
		// ShowWindDriftRangeMarkers Property
		//============================================================================*

		public bool ShowWindDriftRangeMarkers
			{
			get
				{
				return (m_fShowWindDriftRangeMarkers);
				}
			set
				{
				m_fShowWindDriftRangeMarkers = value;
				}
			}

		//============================================================================*
		// SupplyPrintAll Property
		//============================================================================*

		public bool SupplyPrintAll
			{
			get
				{
				return (m_fSupplyPrintAll);
				}
			set
				{
				m_fSupplyPrintAll = value;
				}
			}

		//============================================================================*
		// SupplyPrintBelowStock Property
		//============================================================================*

		public bool SupplyPrintBelowStock
			{
			get
				{
				return (m_fSupplyPrintBelowStock);
				}
			set
				{
				m_fSupplyPrintBelowStock = value;
				}
			}

		//============================================================================*
		// SupplyPrintChecked Property
		//============================================================================*

		public bool SupplyPrintChecked
			{
			get
				{
				return (m_fSupplyPrintChecked);
				}
			set
				{
				m_fSupplyPrintChecked = value;
				}
			}

		//============================================================================*
		// SupplyPrintNonZero Property
		//============================================================================*

		public bool SupplyPrintNonZero
			{
			get
				{
				return (m_fSupplyPrintNonZero);
				}
			set
				{
				m_fSupplyPrintNonZero = value;
				}
			}

		//============================================================================*
		// TaxRate Property
		//============================================================================*

		public double TaxRate
			{
			get
				{
				return (m_dTaxRate);
				}
			set
				{
				m_dTaxRate = value;
				}
			}

		//============================================================================*
		// ToolTips Property
		//============================================================================*

		public bool ToolTips
			{
			get
				{
				return (m_fToolTips);
				}
			set
				{
				m_fToolTips = value;
				}
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
		// TrackReloads Property
		//============================================================================*

		public bool TrackReloads
			{
			get
				{
				return (m_fTrackReloads);
				}
			set
				{
				m_fTrackReloads = value;
				}
			}

		//============================================================================*
		// TransactionSortColumn Property
		//============================================================================*

		public int TransactionSortColumn
			{
			get
				{
				return (m_nTransactionSortColumn);
				}
			set
				{
				m_nTransactionSortColumn = value;
				}
			}

		//============================================================================*
		// TransactionSortOrder Property
		//============================================================================*

		public SortOrder TransactionSortOrder
			{
			get
				{
				return (m_TransactionSortOrder);
				}
			set
				{
				m_TransactionSortOrder = value;
				}
			}

		//============================================================================*
		// UseLastPurchase Property
		//============================================================================*

		public bool UseLastPurchase
			{
			get
				{
				return (m_fUseLastPurchase);
				}
			set
				{
				m_fUseLastPurchase = value;
				}
			}
		}
	}
