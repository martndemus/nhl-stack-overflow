﻿using System.Web;

namespace NHLStackOverflow.Tests
{
    public class StubRequest : HttpRequestBase
    {
        string relativeUrl;

        public StubRequest(string relativeUrl)
        {
            this.relativeUrl = relativeUrl;
        }

        public override string AppRelativeCurrentExecutionFilePath
        {
            get { return relativeUrl; }
        }

        public override string PathInfo
        {
            get { return ""; }
        }
    }
}