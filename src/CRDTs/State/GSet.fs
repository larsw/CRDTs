namespace CRDTs.State

type public GSet<'K,'T when 'T: comparison> when 'K: comparison = { elements: Map<'K, Set<'T>> }

module GSet =
  let init =
    { elements = Map.empty }

  let add key set element =
    match Map.tryFind set.elements key with
    | Some x -> { elements = Map.add key (Set.add element x) }
    | None -> { elements = Map.add key Set([element])  }


  let lookup set element =
    {}