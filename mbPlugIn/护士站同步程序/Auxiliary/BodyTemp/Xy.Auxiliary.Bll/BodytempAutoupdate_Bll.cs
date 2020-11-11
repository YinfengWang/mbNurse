// ***********************************************************************
// Assembly         : Xy.Auxiliary.Bll
// Author           : LPD
// Created          : 01-14-2016
//
// Last Modified By : LPD
// Last Modified On : 01-14-2016
// ***********************************************************************
// <copyright file="BodytempAutoupdate_Bll.cs" company="心医国际(西安)">
//     Copyright (c) 心医国际(西安). All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xy.Auxiliary.Dal;
using System.Data;

/// <summary>
/// The Bll namespace.
/// </summary>
namespace Xy.Auxiliary.Bll
{
    /// <summary>
    /// Class BodytempAutoupdate_Bll.
    /// </summary>
    public class BodytempAutoupdate_Bll
    {
        BodytempAutoupdate_Dal bodyAutoUpdate = new BodytempAutoupdate_Dal();
        /// <summary>
        /// 验证上传的文件是否已经存在数据库
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool fileIsExit(string fileName)
        {
            return bodyAutoUpdate.fileIsExit(fileName);
        }

        /// <summary>
        /// 获取当前文件的版本号
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public int GetNowVersion(string fileName)
        {
            return bodyAutoUpdate.GetNowVersion(fileName);
        }

        /// <summary>
        /// 获取BODYTEMP_AUTOUPDATE表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBodytempAutoupdate()
        {
            return bodyAutoUpdate.GetBodytempAutoupdate();
        }
    }
}
