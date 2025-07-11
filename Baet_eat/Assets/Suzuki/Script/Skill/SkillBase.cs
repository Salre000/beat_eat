using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase
{
    public abstract void Initialize();
    public virtual void Execute() { }

    protected string description;
    public string GetDescription() {  return description; }
}
