﻿using System;


namespace R5T.Richmond
{
    /// <summary>
    /// Empty class that services as an instantiation for extension methods.
    /// </summary>
    public class ApplicationBuilder : ServiceProviderBuilder
    {
        #region Static

        public static new ApplicationBuilder New()
        {
            var applicationBuilder = new ApplicationBuilder();
            return applicationBuilder;
        }

        #endregion
    }
}
