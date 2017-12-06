namespace CRDTs.State

type public PNCounter<'K when 'K: comparison> = { positives: GCounter<'K>; negatives: GCounter<'K> }

module public PNCounter =
  val init<'K when 'K: comparison> : unit -> PNCounter<'K>
  val increment<'K when 'K: comparison> : 'K -> PNCounter<'K> -> PNCounter<'K>
  val decrement<'K when 'K: comparison> : 'K -> PNCounter<'K> -> PNCounter<'K>
  val query<'K when 'K: comparison> : PNCounter<'K> -> uint32
  val compare<'K when 'K: comparison> : PNCounter<'K> -> PNCounter<'K> -> bool
  val merge<'K when 'K: comparison> : PNCounter<'K> -> PNCounter<'K> -> PNCounter<'K>
