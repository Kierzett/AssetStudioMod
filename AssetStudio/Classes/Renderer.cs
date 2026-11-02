using System.Collections.Generic;

namespace AssetStudio
{
    public class StaticBatchInfo
    {
        public ushort firstSubMesh;
        public ushort subMeshCount;

        public StaticBatchInfo(ObjectReader reader)
        {
            firstSubMesh = reader.ReadUInt16();
            subMeshCount = reader.ReadUInt16();
        }
    }

    public abstract class Renderer : Component
    {
        public List<PPtr<Material>> m_Materials;
        public StaticBatchInfo m_StaticBatchInfo;
        public uint[] m_SubsetIndices;

        protected Renderer(ObjectReader reader) : base(reader)
        {
            // Ajustado al typetree de 2022.3.62f1 (Aniimo)
            var m_Enabled = reader.ReadBoolean();
            var m_CastShadows = reader.ReadByte();
            var m_ReceiveShadows = reader.ReadByte();
            var m_DynamicOccludee = reader.ReadByte();
            var m_IsOccluder = reader.ReadByte();
            var m_StaticShadowCaster = reader.ReadByte();
            var m_MotionVectors = reader.ReadByte();
            var m_RayTracingMode = reader.ReadByte();
            var m_RayTraceProcedural = reader.ReadByte();
            reader.AlignStream();

            var m_RenderingLayerMask = reader.ReadUInt32();
            var m_RendererPriority = reader.ReadInt32();

            var m_MaterialsSize = reader.ReadInt32();
            m_Materials = new List<PPtr<Material>>();
            for (var i = 0; i < m_MaterialsSize; i++)
            {
                m_Materials.Add(new PPtr<Material>(reader));
            }
            reader.AlignStream();

            var m_SortingLayerID = reader.ReadInt32();
            var m_SortingLayer = reader.ReadInt16();
            var m_SortingOrder = reader.ReadInt16();
            reader.AlignStream();
        }
    }
}
