using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetLive.Framework.DependencyManagement
{
    /// <summary>
    /// 各个项目内用于组件注册的接口, 系统启动之后会自动扫描所有继承自这个接口的实现类
    /// </summary>
    public interface IDependencyRegister
    {
    }
}
