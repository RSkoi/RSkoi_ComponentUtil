﻿using System.Collections.Generic;
using UnityEngine;

namespace RSkoi_ComponentUtil.UI
{
    internal static partial class ComponentUtilUI
    {
        #region list entry pools
        internal readonly static List<GenericUIListEntry> TransformListEntries = [];
        internal readonly static List<GenericUIListEntry> ComponentListEntries = [];
        internal readonly static List<GenericUIListEntry> ComponentAdderListEntries = [];
        // TODO: these are not pooled yet
        internal readonly static List<PropertyUIEntry> _componentPropertyListEntries = [];
        internal readonly static List<PropertyUIEntry> _componentFieldListEntries = [];
        internal readonly static List<PropertyUIEntry> _objectPropertyListEntries = [];
        internal readonly static List<PropertyUIEntry> _objectFieldListEntries = [];
        #endregion list entry pools

        /// <summary>
        /// clears the pooled ui components, use sparingly (only when scene is reset)
        /// </summary>
        public static void ClearAllEntryPools()
        {
            ClearEntryListData(TransformListEntries);
            ClearEntryListData(ComponentListEntries);
            ClearEntryListData(ComponentAdderListEntries);

            ClearInspectorEntryPools();
        }

        /// <summary>
        /// clears the non-pooled inspector ui components, use sparingly
        /// </summary>
        public static void ClearInspectorEntryPools()
        {
            ClearEntryListGO(_componentPropertyListEntries);
            ClearEntryListGO(_componentFieldListEntries);
            ClearEntryListGO(_objectPropertyListEntries);
            ClearEntryListGO(_objectFieldListEntries);
        }

        #region transform pool
        internal static void PrepareTransformPool(int newEntriesCount)
        {
            ResetAndDisableTransformEntries();

            // instantiate new entries if needed
            if (newEntriesCount > TransformListEntries.Count)
                InstantiateGenericListEntries(
                    newEntriesCount - TransformListEntries.Count,
                    TransformListEntries,
                    _genericListEntryPrefab,
                    _transformListContainer);
        }

        private static void ResetAndDisableTransformEntries()
        {
            foreach (var entry in TransformListEntries)
            {
                entry.UiGO.SetActive(false);
                entry.ResetBgAndChildren();
            }
        }
        #endregion transform pool

        #region component pool
        internal static void PrepareComponentPool(int newEntriesCount)
        {
            ResetAndDisableComponentEntries();

            // instantiate new entries if needed
            if (newEntriesCount > ComponentListEntries.Count)
                InstantiateGenericListEntries(
                    newEntriesCount - ComponentListEntries.Count,
                    ComponentListEntries,
                    _genericListEntryPrefab,
                    _componentListContainer);
        }

        private static void ResetAndDisableComponentEntries()
        {
            foreach (var entry in ComponentListEntries)
            {
                entry.UiGO.SetActive(false);
                entry.ResetBgAndChildren();
            }
        }
        #endregion component pool

        #region component adder pool
        internal static void PrepareComponentAdderPool(int newEntriesCount)
        {
            ResetAndDisableComponentAdderEntries();

            // instantiate new entries if needed
            if (newEntriesCount > ComponentAdderListEntries.Count)
                InstantiateGenericListEntries(
                    newEntriesCount - ComponentAdderListEntries.Count,
                    ComponentAdderListEntries,
                    _genericListEntryPrefab,
                    _componentAdderListContainer);
        }

        private static void ResetAndDisableComponentAdderEntries()
        {
            foreach (var entry in ComponentAdderListEntries)
            {
                entry.UiGO.SetActive(false);
                entry.ResetBgAndChildren();
            }
        }
        #endregion component adder pool

        #region generic
        private static void InstantiateGenericListEntries(
            int count,
            List<GenericUIListEntry> pool,
            GameObject prefab,
            Transform contentContainer)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject entryGO = GameObject.Instantiate(prefab, contentContainer);
                entryGO.SetActive(false);
                // order of disabled pool entries shouldn't matter
                //entryGO.transform.SetAsLastSibling();
                GenericUIListEntry uiEntry = PreConfigureNewGenericUIListEntry(entryGO);
                pool.Add(uiEntry);
            }
        }

        internal static void ClearEntryListData(List<GenericUIListEntry> list)
        {
            foreach (var t in list)
                GameObject.Destroy(t.UiGO);
            list.Clear();
        }

        internal static void ClearEntryListGO(List<PropertyUIEntry> list)
        {
            foreach (var t in list)
                GameObject.Destroy(t.UiGO);
            list.Clear();
        }
        #endregion generic
    }
}
