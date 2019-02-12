using Core.Entity.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Spec
{
    public sealed class CartSpec : BaseSpec<Cart>
    {
        public CartSpec(int cartId)
            : base(b => b.Id == cartId)
        {
            AddInclude(b => b.Items);
        }
        public CartSpec(string userId)
            : base(b => b.UserId == userId)
        {
            AddInclude(b => b.Items);
        }
    }
}
