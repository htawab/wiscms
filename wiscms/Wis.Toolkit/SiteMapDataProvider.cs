//------------------------------------------------------------------------------
// <copyright file="SiteMapDataProvider.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Configuration.Provider;
using System.Collections.Specialized;

namespace Wis.Toolkit
{
    /// <summary>
    /// 站点地图提供程序。
    /// </summary>
    public class SiteMapDataProvider : StaticSiteMapProvider
    {
        private SiteMapNode _RootNode = null;

        /// <summary>
        /// 初始化 SiteMapDataProvider 类的新实例。
        /// </summary>
        public SiteMapDataProvider() { }

        /// <summary>
        /// 初始化 System.Web.SiteMapProvider 实现（包括从持久性存储区加载站点地图数据所需的任何资源）。
        /// </summary>
        /// <param name="name">要初始化的提供程序的 System.Configuration.Provider.ProviderBase.Name。</param>
        /// <param name="attributes">System.Collections.Specialized.NameValueCollection，其中可以包含附加属性以帮助初始化提供程序。从Web.config 文件中的站点地图提供程序配置读取这些属性。</param>
        public override void Initialize(string name, NameValueCollection attributes)
        {
            base.Initialize(name, attributes);

            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/")) applicationPath += "/";
            string url = string.Format("{0}", applicationPath);
            _RootNode = new SiteMapNode(this, "首页", url, "首页");
            AddNode(_RootNode);
        }

        /// <summary>
        /// 从持久性存储区加载站点地图信息，并在内存中构建它。
        /// </summary>
        /// <returns>站点地图导航结构的根 System.Web.SiteMapNode。</returns>
        public override SiteMapNode BuildSiteMap()
        {
            return _RootNode;
        }

        /// <summary>
        /// 获取当前提供程序表示的站点地图数据的根 System.Web.SiteMapNode 对象。返回当前站点地图数据提供程序的根 System.Web.SiteMapNode。默认的实现在被返回的节点上执行安全修整。
        /// </summary>
        public override SiteMapNode RootNode
        {
            get { return _RootNode; }
        }

        /// <summary>
        /// 检索目前由当前提供者管理的所有节点的根节点。
        /// </summary>
        /// <returns>System.Web.SiteMapNode，表示当前提供程序所管理的节点集的根节点。</returns>
        protected override SiteMapNode GetRootNodeCore()
        {
            return RootNode;
        }

        /// <summary>
        /// 检索表示位于指定 URL 的页的 System.Web.SiteMapNode 对象。
        /// </summary>
        /// <param name="rawUrl">标识要为其检索 System.Web.SiteMapNode 的页的 URL。</param>
        /// <returns>返回表示由 rawURL 标识的页的 System.Web.SiteMapNode；如果未找到对应的 System.Web.SiteMapNode，或者如果启用了安全修整并且不能为当前用户返回System.Web.SiteMapNode，则为 null。</returns>
        public override SiteMapNode FindSiteMapNode(string rawUrl)
        {
            return base.FindSiteMapNode(rawUrl);
        }


        /// <summary>
        /// 在根节点下添加一个节点 System.Web.SiteMapNode。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="uri">网址。</param>
        /// <returns>返回添加到提供程序维护的节点集合的 System.Web.SiteMapNode。</returns>
        public SiteMapNode Stack(string title, string uri)
        {
            return Stack(title, uri, _RootNode);
        }

        // stack a node under any other node
        /// <summary>
        /// 在指定的父节点上添加一个节点 System.Web.SiteMapNode。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="uri">网址。</param>
        /// <param name="parentnode">父节点。</param>
        /// <returns>返回添加到提供程序维护的节点集合的 System.Web.SiteMapNode。</returns>
        public SiteMapNode Stack(string title, string uri, SiteMapNode parentnode)
        {
            lock (this)
            {
                SiteMapNode node = base.FindSiteMapNodeFromKey(uri);

                if (node == null)
                {
                    node = new SiteMapNode(this, uri, uri, title);
                    node.ParentNode = ((parentnode == null) ? _RootNode : parentnode);
                    AddNode(node);
                }
                else if (node.Title != title)
                {
                    node.Title = title;
                }

                return node;
            }
        }


        /// <summary>
        /// 在根节点上添加节点集合。
        /// </summary>
        /// <param name="nodes">节点集合。</param>
        public void Stack(List<KeyValuePair<string, Uri>> nodes)
        {
            SiteMapNode parent = RootNode;
            foreach (KeyValuePair<string, Uri> node in nodes)
            {
                parent = Stack(node.Key, node.Value.PathAndQuery, parent);
            }
        }
    }
}
