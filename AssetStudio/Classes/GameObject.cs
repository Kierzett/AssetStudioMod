using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AssetStudio
{
    public sealed class GameObject : EditorExtension
    {
        public List<PPtr<Component>> m_Components;
        public string m_Name;

        public Transform m_Transform;
        public MeshRenderer m_MeshRenderer;
        public MeshFilter m_MeshFilter;
        public SkinnedMeshRenderer m_SkinnedMeshRenderer;
        public Animator m_Animator;
        public Animation m_Animation;
        [JsonIgnore]
        public CubismModel CubismModel;

        public GameObject(ObjectReader reader) : base(reader)
        {
            var m_ComponentSize = reader.ReadInt32();
            m_Components = new List<PPtr<Component>>();
            for (var i = 0; i < m_ComponentSize; i++)
            {
                if (version < (5, 5)) //5.5 down
                {
                    var first = reader.ReadInt32();
                }
                m_Components.Add(new PPtr<Component>(reader));
            }

            m_Name = reader.ReadAlignedString();
            var m_Tag = reader.ReadUInt16();
            var m_Layer = reader.ReadByte();
            var m_IsActive = reader.ReadBoolean();
        }
    }
}
