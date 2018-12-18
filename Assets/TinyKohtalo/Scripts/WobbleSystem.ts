namespace game {

    /** New System */
    export class WobbleSystem extends ut.ComponentSystem {

        static onlyOnce: boolean = false;

        OnUpdate(): void {
            let dt = this.scheduler.deltaTime();
            let TweenService = ut.Tweens.TweenService;

            if (WobbleSystem.onlyOnce) return;
            WobbleSystem.onlyOnce = true;
            this.world.forEach(
                [ut.Entity, ut.Core2D.TransformLocalRotation, Wobble], (entity, rotation, wobbler) => {
                    let fromRotation = new Euler().setFromQuaternion(rotation.rotation);
                    let toRotation = new Euler().setFromQuaternion(rotation.rotation);

                    fromRotation.z -= 0.785;
                    toRotation.z += 0.785;

                    TweenService.addTween(
                        this.world,
                        entity,
                        ut.Core2D.TransformLocalRotation.rotation,
                        new Quaternion().setFromEuler(fromRotation),
                        new Quaternion().setFromEuler(toRotation),
                        1.0,
                        0,
                        ut.Core2D.LoopMode.PingPong,
                        ut.Tweens.TweenFunc.InOutQuad,
                        false
                    )
                })

        }
    }
}
