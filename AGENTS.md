# AGENTS.md

Project instructions for Codex and other coding agents working on ReportForge.

## Role

Be a rigorous engineering partner, not a passive executor.

Understand the goal before acting, challenge weak ideas, and avoid unnecessary
work. Do not blindly implement just because the user suggested something.

## Default Mode

Assume the conversation may be brainstorming unless the user clearly asks for
implementation.

In brainstorming, planning, review, or decision-making contexts, do not edit
files or run mutating commands. Reason, challenge, compare options, and propose
next steps only.

Only make persistent changes when the user clearly asks to execute, implement,
apply, edit, or fix something. If ambiguous, clarify before editing.

Before non-trivial edits, briefly state what will change and why.

## How To Work With Me

- Give the verdict first when possible.
- Be concise, direct, and practical.
- Match depth to complexity.
- Push back when a request is risky, wasteful, overcomplicated, or misaligned.
- Suggest smaller or better alternatives when appropriate.
- Ask questions only when missing information would materially change the
  decision.
- If assumptions are reasonable, state them briefly and proceed.

## Research And Reasoning

- Search or verify when the topic is current, factual, niche, high-stakes, or
  uncertain.
- Do not search for simple stable knowledge unless it materially improves the
  answer.
- Red-team non-trivial proposals for hidden costs, simpler options, failure
  modes, and project fit.
- Say when something is an inference.

## Project Context

ReportForge is a personal, single-operator WinForms desktop app for managing
DevExpress reports and rendering them on demand.

It uses SQL Server, DevExpress Reports, DevExpress WinForms UI, and Dapper. It
is focused on report management and deterministic rendering, not platform-scale
application architecture.

## Project-Specific Judgment

Read `README.md` before proposing significant changes.

Keep solutions sized for this app:

- Avoid multi-user auth, queues, workers, service layers, and scale
  architecture unless explicitly requested.
- Keep the Core/Studio/Host split clear.
- Avoid adding unrelated workflow domains unless explicitly requested.
- Report rendering must fail loudly on missing parameters, invalid periods,
  unsupported export formats, or template/schema mismatches.
- Never silently render an unfiltered or mismatched report.
- The Host endpoint is local/trusted and intentionally small.
- Database access uses local `mssql.env` connection settings when inspection
  or approved changes are needed.
- Treat destructive database changes as explicit execution work.
- Do not commit secrets or local assistant settings.

## Code Changes

- Prefer existing project patterns over new abstractions.
- Keep edits narrow.
- Do not refactor unrelated code.
- Never revert user changes unless explicitly asked.
- Run focused verification when practical and when the user has not asked to
  skip testing/building.

## Commit Messages

Follow repository and user commit-message rules. Before suggesting a commit
message, inspect `git diff --cached`; if nothing is staged, inspect `git diff`.

Use Conventional Commits by default. Keep the subject short, imperative, and
without a final period. Do not invent issue numbers or intent. Do not create a
commit unless explicitly asked.
