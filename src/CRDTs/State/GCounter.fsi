namespace CRDTs.State

type public GCounter<'K> when 'K : comparison = { payload: Map<'K, uint32> }

module public GCounter =
  val init : GCounter<'K>
  val increment : 'K -> GCounter<'K> -> GCounter<'K>
  val query : GCounter<'K> -> uint32
  val compare : GCounter<'K> -> GCounter<'K> -> bool
  val merge : GCounter<'K> -> GCounter<'K> -> GCounter<'K>
