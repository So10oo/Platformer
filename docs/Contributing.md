# Contributing & documentation maintenance

This project keeps documentation **inside the repo**. The rule is simple:

> If you change behavior/architecture, you update docs in the same PR.

## What to update when you change code

### If you add/change a system

- Update `docs/Architecture.md` (module list + flow)
- Update `docs/TechStack.md` if packages/plugins changed
- Add/extend a system doc under `docs/` (create a new page if needed)

### If you change DI / Zenject bindings

- Update `docs/DI-Zenject.md`
- If the binding affects lifecycle (creation/disposal), explicitly document it

### If you change hero movement or inputs

- Update `docs/Gameplay/HeroStateMachine.md`
- Update `docs/Runbook.md` if run/build steps or debug toggles changed

## ADR (Architecture Decision Records)

When you make a design decision that will matter in a month, record it:

- Create a new file: `docs/ADR/NNNN-short-title.md`
- Use the template in `docs/ADR/0000-template.md`
- Keep it short: context, decision, consequences

## “Docs are stale” checklist (triage)

If someone reports docs are outdated:

- Identify the code location that diverged
- Update the relevant doc page(s)
- Add an ADR if the change was a deliberate design shift
