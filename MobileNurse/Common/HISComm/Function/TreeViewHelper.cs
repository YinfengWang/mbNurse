using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HISPlus
{
    public class TreeViewHelper
    {
        public TreeViewHelper()
        {
        }
        
        
        /// <summary>
        /// 向上移动节点
        /// </summary>
        /// <param name="trv"></param>
        /// <param name="node"></param>
        public static bool NodeMoveUp(ref TreeView trv, ref TreeNode node)
        {
            if (trv == null || node == null)
            {
                return false;
            }
            
            int         nodeIndex   = node.Index - 1;                   // 移动后节点的位置
            TreeNode    nodeNew     = new TreeNode();
            
            // 如果为最顶节点
            if (node.Index == 0)
            {
                return false;
            }
            else
            {
                // 创建Clone
                nodeNew = (TreeNode)node.Clone();
                
                // 添加节点
                if (node.Level == 0)
                {
                    trv.Nodes.Insert(node.PrevNode.Index, nodeNew);
                    
                    node.Remove();                                      // 移除节点
                    
                    node = trv.Nodes[nodeIndex];
                }
                else
                {
                    TreeNode nodeParent = node.Parent;
                    
                    nodeParent.Nodes.Insert(node.PrevNode.Index, nodeNew);
                    
                    node.Remove();                                      // 移除节点
                    
                    node = nodeParent.Nodes[nodeIndex];  
                }
                
                return true;
            }
        }
        
        
        /// <summary>
        /// 向下移动节点
        /// </summary>
        /// <param name="trv"></param>
        /// <param name="node"></param>
        public static bool NodeMoveDown(ref TreeView trv, ref TreeNode node)
        {
            if (trv == null || node == null)
            {
                return false;
            }
            
            int      nodeIndex  = node.Index + 1;                       // 移动后节点的位置
            TreeNode nodeNew    = new TreeNode();
            
            // 如果选中的是根节点  
            if (node.Level == 0)  
            {  
                // 如果选中的不是最底的节点
                if (node.Index < trv.Nodes.Count - 1)  
                {  
                    nodeNew = (TreeNode)node.Clone();  
                    
                    trv.Nodes.Insert(node.NextNode.Index + 1, nodeNew);  
                    
                    node.Remove();
                    node = trv.Nodes[nodeIndex];
                    
                    return true;
                }  
            }
            // 如果选中节点不是根节点  
            else
            {  
                // 如果选中的不是最低的节点  
                TreeNode nodeParent = node.Parent;
                
                if (node.Index < nodeParent.Nodes.Count - 1)  
                {                  
                    nodeNew = (TreeNode)node.Clone();  
                    
                    nodeParent.Nodes.Insert(node.NextNode.Index + 1, nodeNew);  
                    
                    node.Remove();
                    node = nodeParent.Nodes[nodeIndex];
                    
                    return true;
                }  
            }
            
            return false;
        }
    }
}
