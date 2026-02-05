using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Hud.TurretUi
{
    public sealed class TurretBuilderView : ViewBase<TurretBuilderViewModel>
    {
        [SerializeField]
        private Button _buildGunner;

        [SerializeField]
        private Button _buildCannon;

        [SerializeField]
        private Button _buildFrost;

        protected override void Bind()
        {
            base.Bind();

            _buildGunner.onClick.AddListener(() => ViewModel.SpawnEnemy(EnemyType.Tank));
            //_buildGunner.onClick.AddListener(() => ViewModel.BuildTurret(TowerType.Gunner));
            _buildCannon.onClick.AddListener(() => ViewModel.BuildTurret(TowerType.Cannon));
            _buildFrost.onClick.AddListener(() => ViewModel.BuildTurret(TowerType.Frost));
        }
    }
}