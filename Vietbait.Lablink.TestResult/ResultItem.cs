using System;

namespace Vietbait.Lablink.TestResult
{
    public class ResultItem
    {
        public string MeasureUnit { get; set; }
        public int DataSequence { get; set; }
        public string NormalLevel { get; set; }
        public string NormalLevelW { get; set; }
        public string TestName { get; set; }
        public string TestDataId { get; set; }
        public string TestValue { get; set; }
        public short TestTypeId { get; set; }

        #region Contructor

        public ResultItem()
            : this(String.Empty)
        {
        }

        public ResultItem(string testName)
            : this(testName, "")
        {
        }

        public ResultItem(string testName, string testValue)
            : this(testName, testValue, "")
        {
        }

        public ResultItem(string testName, string testValue, string mearsureUnit)
            : this(testName, testValue, mearsureUnit, "")
        {
        }

        public ResultItem(string testName, string testValue, string mearsureUnit, string normalLevel)
            : this(testName, testValue, mearsureUnit, normalLevel, "")
        {
        }

        public ResultItem(string testName, string testValue, string mearsureUnit, string normalLevel,
                          string normalLevelW)
            : this(testName, testValue, mearsureUnit, normalLevel, normalLevelW, -1)
        {
        }

        public ResultItem(string testName, string testValue, string mearsureUnit, string normalLevel,
                          string normalLevelW, int dataSequence)
            : this(testName, testValue, mearsureUnit, normalLevel, normalLevelW, -1, -1)
        {
        }

        public ResultItem(string testName, string testValue, string mearsureUnit, string normalLevel,
                          string normalLevelW, int dataSequence, short testTypeId)
        {
            TestName = testName;
            TestDataId = testName;
            TestValue = testValue;
            MeasureUnit = mearsureUnit;
            NormalLevel = normalLevel;
            NormalLevelW = normalLevelW;
            DataSequence = dataSequence;
            TestTypeId = testTypeId;
        }

        #endregion

        #region Overrides Function

        public override string ToString()
        {
            return String.Format("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}\t\t{5}\t\t{6}\t\t{7}", TestName, TestValue,
                                 MeasureUnit, DataSequence, NormalLevel, NormalLevelW, TestTypeId, TestDataId);
        }

        #endregion
    }
}