--1ST Q

SELECT
p.FullName AS ProviderName,
d.Name AS DepartmentName,
COUNT(e.EncounterId) AS TotalEncounters,
RANK() OVER(ORDER BY COUNT(e.EncounterId) DESC) AS ProviderRank
from Provider p
JOIN Department d
  on p.DepartmentId = d.DepartmentId
LEFT JOIN Encounter e
  on p.ProviderId = e.ProviderId
GROUP BY
  p.FullName,
  d.Name
ORDER BY
  TotalEncounters DESC;

  --2nd Q

ALTER TABLE Insurance
ADD
    ValidFrom DATETIME2
        GENERATED ALWAYS AS ROW START HIDDEN
        CONSTRAINT DF_Insurance_From
        DEFAULT SYSUTCDATETIME(),

    ValidTo DATETIME2
        GENERATED ALWAYS AS ROW END HIDDEN
        CONSTRAINT DF_Insurance_To
        DEFAULT '9999-12-31 23:59:59.9999999',

    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

	ALTER TABLE Insurance
SET (
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.Insurance_History
    )
);

UPDATE Insurance
SET Payer = 'BlueCross',
    PolicyNumber = 'BC9988'
WHERE PatientId = 10;

SELECT
    i.InsuranceId,
    i.Payer,
    i.PolicyNumber,
    i.ValidFrom,
    i.ValidTo
FROM Insurance
FOR SYSTEM_TIME ALL AS i
WHERE i.PatientId = 10
ORDER BY i.ValidFrom;

--3rd Q

CREATE VIEW Billing_Claim_View
AS
SELECT
    c.ClaimId,
    c.EncounterId,
    c.InsuranceId,
    c.BilledAmount,
    c.ReimbursedAmt,
    c.Status
FROM Claim c;

CREATE PROCEDURE usp_MonthlyRevenueLeakageReport
AS
BEGIN
    SELECT
        Status,
        COUNT(*) AS TotalClaims,
        SUM(BilledAmount) AS TotalBilledAmount,
        SUM(ReimbursedAmt) AS TotalReimbursedAmount,
        SUM(BilledAmount - ISNULL(ReimbursedAmt, 0)) AS OutstandingAmount,
        RANK() OVER (
            ORDER BY SUM(BilledAmount - ISNULL(ReimbursedAmt, 0)) DESC
        ) AS LossRank
    FROM Billing_Claim_View
    GROUP BY Status
    ORDER BY OutstandingAmount DESC;
END;

--4th Q

CREATE PROCEDURE usp_ExecutiveDashboard
AS
BEGIN

  
    SELECT
        'Total Active Patients' AS Metric,
        COUNT(*) AS Value
    FROM Patient
    WHERE IsActive = 1

    UNION ALL

  
    SELECT
        CONCAT('Top Dept: ', d.Name) AS Metric,
        COUNT(e.EncounterId) AS Value
    FROM Encounter e
    JOIN Department d ON e.DepartmentId = d.DepartmentId
    GROUP BY d.Name
    ORDER BY Value DESC
    OFFSET 0 ROWS FETCH NEXT 5 ROWS ONLY

    UNION ALL

   
    SELECT
        'Denied Claims' AS Metric,
        COUNT(*) AS Value
    FROM Claim
    WHERE Status = 'Denied';

END;



 



