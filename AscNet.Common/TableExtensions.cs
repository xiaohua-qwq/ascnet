﻿using AscNet.Table.V2.share.item;

namespace AscNet.Common
{
    public static class TableExtensions
    {
        public static int GetCharacterExp(this ItemTable item, int cardType)
        {
            if (item.ItemType == (int)ItemType.CardExp && item.SubTypeParams.Count >= 3)
            {
                int upType = item.SubTypeParams[0];
                int exp = item.SubTypeParams[1];
                int upPercentage = item.SubTypeParams[2] / 100 - 100;
                int upMultiple = item.SubTypeParams[2] / 10000;

                if (cardType == upType)
                    return (int)MathF.Round(exp * upMultiple);

                return exp;
            }

            return 0;
        }
    }

    enum ItemType
    {
        Assert = 1 << 0,
        Money = 1 << 1 | 1 << 0,
        Material = 1 << 2,
        Fragment = 1 << 3,
        Gift = 1 << 4,
        WeaponFashion = 1 << 5,
        CardExp = 1 << 11 | 1 << 2,
        EquipExp = 1 << 12 | 1 << 2,
        EquipExpNotInBag = 1 << 12 | 1 << 3,
        EquipResonanace = 1 << 13 | 1 << 2,
        FurnitureItem = 1 << 14 | 1 << 2,
        ExchangeMoney = 1 << 16 | 1 << 2,
        SpExchangeMoney = 1 << 17 | 1 << 2,
        UnShow = 1 << 18 | 1 << 2,
        FavorGift = 1 << 19 | 1 << 2,
        ActiveMoney = 1 << 20 | 1 << 2,
        PlayingMoney = 1 << 21 | 1 << 2,
        PlayingItem = 1 << 22 | 1 << 2,
        TRPGItem = 1 << 23 | 1 << 2,
        PartnerExp = 1 << 25 | 1 << 2
    }
}